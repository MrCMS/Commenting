using MrCMS.Services;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Web.Apps.Commenting.Events;
using MrCMS.Web.Apps.Commenting.MessageTemplates;

namespace MrCMS.Web.Apps.Commenting.Services
{
    public class QueueCommentReportedNotificationEmails : IOnCommentReported
    {
        private readonly IMessageParser<CommentReportedMessageTemplate, Comment> _messageParser;

        public QueueCommentReportedNotificationEmails(IMessageParser<CommentReportedMessageTemplate, Comment> messageParser)
        {
            _messageParser = messageParser;
        }
        public void Execute(CommentReportedEventArgs args)
        {
            var queuedMessage = _messageParser.GetMessage(args.Comment);
            _messageParser.QueueMessage(queuedMessage);
        }
    }
}