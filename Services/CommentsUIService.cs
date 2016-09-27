using System.Collections.Generic;
using System.Linq;
using MrCMS.Entities.Documents.Web;
using MrCMS.Entities.People;
using MrCMS.Helpers;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Web.Apps.Commenting.Helpers;
using MrCMS.Web.Apps.Commenting.Models;
using MrCMS.Web.Apps.Commenting.Settings;
using MrCMS.Web.Apps.Core.Models.RegisterAndLogin;
using MrCMS.Website;
using NHibernate;
using MrCMS.Services.Resources;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace MrCMS.Web.Apps.Commenting.Services
{
    public class CommentsUIService : ICommentsUIService
    {
        public const string PendingApprovalMessage = "Thanks for posting! Your comment is pending approval by admins, and will be shown shortly.";
        private readonly ISession _session;
        private readonly CommentingSettings _settings;
        private readonly IGetWebpageCommentingInfo _getWebpageCommentingInfo;
        private readonly IStringResourceProvider _stringResourceProvider;
        public const string AddedMessage = "Thanks for posting!";

        public CommentsUIService(CommentingSettings settings, IGetWebpageCommentingInfo getWebpageCommentingInfo, ISession session, IStringResourceProvider stringResourceProvider)
        {
            _settings = settings;
            _getWebpageCommentingInfo = getWebpageCommentingInfo;
            _session = session;
            _stringResourceProvider = stringResourceProvider;
        }

        public CommentsViewInfo GetAddCommentsInfo(Webpage webpage)
        {
            if (!webpage.AreCommentsEnabled(GetCommentingInfo(webpage), _settings))
            {
                return new CommentsViewInfo { Disabled = true };
            }
            if (!_settings.AllowGuestComments && CurrentRequestData.CurrentUser == null)
            {
                return new CommentsViewInfo
                {
                    View = "Login",
                    Model = new LoginModel(),
                };
            }
            if (CurrentRequestData.CurrentUser == null)
            {
                return new CommentsViewInfo
                {
                    View = "Guest",
                    Model = new GuestAddCommentModel { WebpageId = webpage.Id },
                };
            }
            return new CommentsViewInfo
            {
                View = "LoggedIn",
                Model = new LoggedInUserAddCommentModel { WebpageId = webpage.Id },
            };
        }

        public CommentsViewInfo GetShowCommentsInfo(Webpage webpage)
        {
            if (!webpage.AreCommentsEnabled(GetCommentingInfo(webpage), _settings))
            {
                return new CommentsViewInfo { Disabled = true };
            }
            var allComments = _session.QueryOver<Comment>().Where(comment => comment.Webpage == webpage && comment.Approved == true)
                    .OrderBy(x => x.CreatedOn).Asc.Cacheable().List();
            var commentIds = allComments.Select(x => x.Id).ToList();
            VoteCounts counts = null;
            var showCommentsInfo = new CommentsViewInfo
            {
                View = "Show",
                Model = new CommentTreeData
                {
                    AllComments = allComments,
                    UpvoteCounts =
                        commentIds.Any()
                            ? _session.QueryOver<Vote>().Where(x => x.IsUpvote && x.Comment.Id.IsIn(commentIds))
                                .SelectList(builder =>
                                {
                                    builder.SelectGroup(x => x.Comment.Id).WithAlias(() => counts.Comment);
                                    builder.SelectCount(x => x.Id).WithAlias(() => counts.Count);
                                    return builder;
                                }).TransformUsing(Transformers.AliasToBean<VoteCounts>()).List<VoteCounts>()
                            : new List<VoteCounts>(),
                    DownvoteCounts =
                        commentIds.Any()
                            ? _session.QueryOver<Vote>().Where(x => !x.IsUpvote && x.Comment.Id.IsIn(commentIds))
                                .SelectList(builder =>
                                {
                                    builder.SelectGroup(x => x.Comment.Id).WithAlias(() => counts.Comment);
                                    builder.SelectCount(x => x.Id).WithAlias(() => counts.Count);
                                    return builder;
                                }).TransformUsing(Transformers.AliasToBean<VoteCounts>()).List<VoteCounts>()
                            : new List<VoteCounts>(),
                    CurrentUser = CurrentRequestData.CurrentUser,
                    Webpage = webpage
                },
                ViewData =
                {
                    ["allow-reply"] = _settings.AllowGuestComments ||
                                      CurrentRequestData.CurrentUser != null,
                    ["webpage"] = webpage
                }
            };
            return showCommentsInfo;
        }


        private CommentingInfo GetCommentingInfo(Webpage webpage)
        {
            return _getWebpageCommentingInfo.Get(webpage);
        }

        public CommentsViewInfo GetReplyToInfo(Comment comment)
        {
            Webpage webpage = comment.Webpage;
            if (webpage == null || !webpage.AreCommentsEnabled(GetCommentingInfo(webpage), _settings))
            {
                return new CommentsViewInfo { Disabled = true };
            }
            if (!_settings.AllowGuestComments && CurrentRequestData.CurrentUser == null)
            {
                return new CommentsViewInfo { Disabled = true };
            }
            if (CurrentRequestData.CurrentUser == null)
            {
                return new CommentsViewInfo
                {
                    View = "GuestReply",
                    Model = new GuestAddCommentModel { WebpageId = webpage.Id, InReplyTo = comment.Id },
                };
            }
            return new CommentsViewInfo
            {
                View = "LoggedInReply",
                Model = new LoggedInUserAddCommentModel { WebpageId = webpage.Id, InReplyTo = comment.Id },
            };
        }

        public PostCommentResponse AddGuestComment(GuestAddCommentModel model)
        {
            if (_settings.AllowGuestComments)
            {
                var comment = new Comment
                {
                    Email = model.Email,
                    Name = model.Name,
                    Message = model.Message,
                    Webpage = GetWebpage(model),
                    Approved =
                                      _settings.CommentApprovalType == CommentApprovalType.None
                                          ? true
                                          : (bool?)null,
                    InReplyTo = GetInReplyTo(model),
                    IPAddress = model.IPAddress,
                    SendReplyNotifications = model.ReplyNotification
                };
                AddNewCommentToReplyChildren(comment);
                _session.Transact(session => session.Save(comment));
                return GetResponse(comment);
            }
            return new PostCommentResponse { Valid = false };
        }

        public PostCommentResponse AddLoggedInComment(LoggedInUserAddCommentModel model)
        {
            User currentUser = CurrentRequestData.CurrentUser;
            if (IsValid(model) && currentUser != null)
            {
                var comment = new Comment
                {
                    User = currentUser,
                    Name = currentUser.GetCommentsInfo().Username,
                    Email = currentUser.Email,
                    Message = model.Message,
                    Webpage = GetWebpage(model),
                    Approved = _settings.CommentApprovalType == CommentApprovalType.All ? (bool?)null : true,
                    InReplyTo = GetInReplyTo(model),
                    IPAddress = model.IPAddress,
                    SendReplyNotifications = model.ReplyNotification
                };
                AddNewCommentToReplyChildren(comment);
                _session.Transact(session => session.Save(comment));
                return GetResponse(comment);
            }
            return new PostCommentResponse { Valid = false };
        }

        private Comment GetInReplyTo(IAddCommentModel model)
        {
            return model.InReplyTo.HasValue
                ? _session.Get<Comment>(model.InReplyTo)
                : null;
        }

        private static void AddNewCommentToReplyChildren(Comment comment)
        {
            if (comment.InReplyTo != null)
            {
                comment.InReplyTo.Children.Add(comment);
            }
        }

        private Webpage GetWebpage(IAddCommentModel model)
        {
            return _session.Get<Webpage>(model.WebpageId);
        }

        private bool IsValid(IAddCommentModel model)
        {
            Webpage webpage = GetWebpage(model);
            if (webpage == null)
                return false;
            Comment comment = GetInReplyTo(model);
            return comment == null || comment.Webpage == webpage;
        }

        private PostCommentResponse GetResponse(Comment comment)
        {
            bool pending = comment.Approved != true;
            return new PostCommentResponse
            {
                Valid = true,
                Pending = pending,
                Message = pending
                           ? _stringResourceProvider.GetValue(PendingApprovalMessage)
                           : _stringResourceProvider.GetValue(AddedMessage),
                RedirectUrl = "~/" + comment.Webpage.LiveUrlSegment
            };
        }
    }

    public class CommentTreeData
    {
        public IList<Comment> AllComments { get; set; }
        public IEnumerable<Comment> RootComments => AllComments.Where(x => x.InReplyTo == null);

        public IEnumerable<Comment> InReplyTo(int id)
            => AllComments.Where(x => x.InReplyTo != null && x.InReplyTo.Id == id);

        public IList<VoteCounts> UpvoteCounts { get; set; }

        public int GetUpvotes(int id) => UpvoteCounts.FirstOrDefault(x => x.Comment == id)?.Count ?? 0;

        public IList<VoteCounts> DownvoteCounts { get; set; }
        public User CurrentUser { get; set; }
        public Webpage Webpage { get; set; }

        public int GetDownvotes(int id) => DownvoteCounts.FirstOrDefault(x => x.Comment == id)?.Count ?? 0;

        public CommentRenderInfo GetRenderInfo(Comment comment,int depth)
        {
            return new CommentRenderInfo
            {
                Comment = comment,
                Upvotes = GetUpvotes(comment.Id),
                Downvotes = GetDownvotes(comment.Id),
                TreeData = this,
                Depth = depth,
                CurrentUser = CurrentUser,
                Webpage = Webpage,
            };
        }

        public class CommentRenderInfo
        {
            public Comment Comment { get; set; }
            public int Upvotes { get; set; }
            public int Downvotes { get; set; }
            public CommentTreeData TreeData { get; set; }
            public int Depth { get; set; }
            public User CurrentUser { get; set; }
            public Webpage Webpage { get; set; }
        }
    }

    public class VoteCounts
    {
        public int Comment { get; set; }
        public int Count { get; set; }
    }
}
