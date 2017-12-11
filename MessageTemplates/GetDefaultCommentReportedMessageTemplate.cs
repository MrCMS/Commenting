using System;
using MrCMS.Messages;

namespace MrCMS.Web.Apps.Commenting.MessageTemplates
{
    public class GetDefaultCommentReportedMessageTemplate : GetDefaultTemplate<CommentReportedMessageTemplate>
    {
        public override CommentReportedMessageTemplate Get()
        {
            return new CommentReportedMessageTemplate
            {
                FromAddress = "test@example.com",
                FromName = "Site Owner",
                ToAddress = "{NotifyCommentAddedEmail}",
                ToName = "",
                Bcc = String.Empty,
                Cc = String.Empty,
                Subject = "A Comment Reported - #{Id}",
                Body = "<p>Comment #{Id} on <a href=\"{PageUrl}\">{PageName}</a> has been reported.</p><p><a href=\"{CommentModerationUrl}\">Comment Moderation</a></p><p>It has been reported by: {ReportedCommentDetails}</p>",
                IsHtml = true
            };
        }
    }
}