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

namespace MrCMS.Web.Apps.Commenting.Services
{
    public class CommentsUIService : ICommentsUIService
    {
        private readonly ISession _session;
        private readonly CommentingSettings _settings;
        private readonly IGetWebpageCommentingInfo _getWebpageCommentingInfo;
        private readonly IStringResourceProvider _stringResourceProvider;

        public CommentsUIService(CommentingSettings settings, IGetWebpageCommentingInfo getWebpageCommentingInfo, ISession session, IStringResourceProvider stringResourceProvider)
        {
            _settings = settings;
            _getWebpageCommentingInfo = getWebpageCommentingInfo;
            _session = session;
            _stringResourceProvider = stringResourceProvider;
        }

        public CommentsViewInfo GetAddCommentsInfo(Webpage webpage)
        {
            if (!webpage.AreCommentsEnabled(_getWebpageCommentingInfo.Get(webpage), _settings))
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
            if (!webpage.AreCommentsEnabled(_getWebpageCommentingInfo.Get(webpage), _settings))
            {
                return new CommentsViewInfo { Disabled = true };
            }
            var showCommentsInfo = new CommentsViewInfo
                                   {
                                       View = "Show",
                                       Model =
                                           _session.QueryOver<Comment>()
                                           .Where(
                                               comment =>
                                           comment.Webpage == webpage && comment.Approved == true &&
                                           comment.InReplyTo == null)
                                           //.Take(_settings.InitialNumberOfCommentsToShow)
                                           .List(),
                                   };
            showCommentsInfo.ViewData["allow-reply"] = _settings.AllowGuestComments ||
                                                       CurrentRequestData.CurrentUser != null;
            showCommentsInfo.ViewData["webpage"] = webpage;
            return showCommentsInfo;
        }

        public CommentsViewInfo GetReplyToInfo(Comment comment)
        {
            Webpage webpage = comment.Webpage;
            if (webpage == null || !webpage.AreCommentsEnabled(_getWebpageCommentingInfo.Get(webpage), _settings))
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
                           ? _stringResourceProvider.GetValue(_settings.CommentPendingApprovalMessage)
                           : _stringResourceProvider.GetValue(_settings.CommentAddedMessage),
                       RedirectUrl = "~/" + comment.Webpage.LiveUrlSegment
                   };
        }
    }
}
