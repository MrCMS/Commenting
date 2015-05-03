using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MrCMS.Entities.Multisite;
using MrCMS.Services;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Web.Apps.Commenting.Pages;
using MrCMS.Web.Apps.Commenting.Settings;

namespace MrCMS.Web.Apps.Commenting.MessageTemplates.TokenProviders
{
    public class CommentTokenProvider : ITokenProvider<Comment>
    {
        private readonly IUniquePageService _uniquePageService;
        private readonly CommentingSettings _commentingSettings;
        private readonly Site _site;

        private IDictionary<string, Func<Comment, string>> _tokens;
        public IDictionary<string, Func<Comment, string>> Tokens { get { return _tokens = _tokens ?? GetTokens(); } }

        public CommentTokenProvider(IUniquePageService uniquePageService,
            CommentingSettings commentingSettings, Site site)
        {
            _uniquePageService = uniquePageService;
            _commentingSettings = commentingSettings;
            _site = site;
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
                {"CommentUnsubscribeUrl", comment => comment.InReplyTo != null ? string.Format("{0}?guid={1}", _uniquePageService.GetUniquePage<UnsubscribeFromCommentReplies>().AbsoluteUrl, comment.InReplyTo.Guid) : null},
                {"CommentModerationUrl", 
                    comment => string.Format("http://{0}/Admin/Apps/Commenting/Comment?Id={1}", _site.BaseUrl, comment.Id)}
            };
        }
    }
}