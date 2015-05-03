using MrCMS.Messages;

namespace MrCMS.Web.Apps.Commenting.MessageTemplates
{
    public class GetDefaultNewCommentReplyMessageTemplate : GetDefaultTemplate<NewCommentReplyMessageTemplate>
    {
        public override NewCommentReplyMessageTemplate Get()
        {
            return new NewCommentReplyMessageTemplate
            {
                FromName = "{SiteName}",
                FromAddress = "admin@yoursite.com",
                ToName = "{CommentNotificationName}",
                ToAddress = "{CommentNotificationEmail}",
                Bcc = string.Empty,
                Cc = string.Empty,
                Subject = "{SiteName} New Reply To Comment.",
                Body =
                    "<p>You have received a reply to your comment on {SiteName}, <a href='{PageUrl}'>click here</a> to return to the story.</p>" +
                    "<p>If you wish to stop receiving notifications when someone replies to this comment <a href='{CommentUnsubscribeUrl}'>click here</a>.</p>",
                IsHtml = true
            };
        }
    }
}