using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MrCMS.Entities.Multisite;
using MrCMS.Helpers;
using MrCMS.Services;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Web.Apps.Commenting.Pages;
using MrCMS.Web.Apps.Commenting.Settings;
using NHibernate;

namespace MrCMS.Web.Apps.Commenting.MessageTemplates.TokenProviders
{
    public class CommentTokenProvider : ITokenProvider<Comment>
    {
        private readonly IUniquePageService _uniquePageService;
        private readonly CommentingSettings _commentingSettings;
        private readonly Site _site;
        private readonly ISession _session;

        private IDictionary<string, Func<Comment, string>> _tokens;
        public IDictionary<string, Func<Comment, string>> Tokens { get { return _tokens = _tokens ?? GetTokens(); } }

        public CommentTokenProvider(IUniquePageService uniquePageService,
            CommentingSettings commentingSettings, Site site, ISession session)
        {
            _uniquePageService = uniquePageService;
            _commentingSettings = commentingSettings;
            _site = site;
            _session = session;
        }

        private IDictionary<string, Func<Comment, string>> GetTokens()
        {
            return new Dictionary<string, Func<Comment, string>>
            {
                {"PageUrl", comment => comment.Webpage != null ? comment.Webpage.AbsoluteUrl : null},
                {"PageName", comment => comment.Webpage != null ? comment.Webpage.Name : null},
                {"NotifyCommentAddedEmail", comment => _commentingSettings.NotifyCommentAddedEmail},
                {"CommentNotificationEmail", comment => comment.InReplyTo != null ? comment.InReplyTo.Email : null},
                {"CommentNotificationName", comment => comment.InReplyTo != null ? comment.InReplyTo.Name : null},
                {"CommentUnsubscribeUrl", comment => comment.InReplyTo != null ? string.Format("{0}?guid={1}", _uniquePageService.GetUniquePage<UnsubscribeFromCommentReplies>()?.AbsoluteUrl, comment.InReplyTo.Guid) : null},
                {"CommentModerationUrl", 
                    comment => string.Format("https://{0}/Admin/Apps/Commenting/Comment?Id={1}", _site.BaseUrl, comment.Id)},
                {
                    "ReportedCommentDetails", comment =>
                    {
                        var reportedComment = _session.QueryOver<ReportedComment>()
                            .Where(x => x.Comment.Id == comment.Id).OrderBy(x => x.Id).Desc.Take(1).SingleOrDefault();
                        if (reportedComment == null)
                            return string.Empty;
                        var reportedCommentDetails = string.Empty;
                        if (reportedComment.User != null)
                            reportedCommentDetails +=
                                $"The comment has been reported by the user <a href='https://{_site.BaseUrl}/admin/user/edit/{reportedComment.User.Id}' target='_blank'>" +
                                reportedComment.User.Email + "</a>" + "</br></br>";
                        else if (!string.IsNullOrWhiteSpace(reportedComment.IPAddress))
                            reportedCommentDetails +=
                                $"The comment has been reported via this IP address: " + reportedComment.IPAddress + "</br></br>";
                        reportedCommentDetails += $"<a href=\"https://{_site.BaseUrl}/admin/apps/commenting/reportedcomment/show/{reportedComment.Id}\">Reported comment details</a>";
                        return reportedCommentDetails;
                    }
                }
            };
        }
    }
}