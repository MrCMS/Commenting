using System;
using MrCMS.Messages;

namespace MrCMS.Web.Apps.Commenting.MessageTemplates
{
    public class GetDefaultCommentAddedMessageTemplate : GetDefaultTemplate<CommentAddedMessageTemplate>
    {
        public override CommentAddedMessageTemplate Get()
        {
            return new CommentAddedMessageTemplate
            {
                FromAddress = "test@example.com",
                FromName = "{SiteName}",
                ToAddress = "{NotifyCommentAddedEmail}",
                ToName = "",
                Bcc = String.Empty,
                Cc = String.Empty,
                Subject = "Comment Posted on {PageName}",
                Body = "<p>A new comment has been posted on <a href=\"{PageUrl}\">{PageName}</a>.</p>",
                IsHtml = true
            };
        }
    }
}