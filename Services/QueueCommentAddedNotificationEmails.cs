using MrCMS.Events;
using MrCMS.Services;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Web.Apps.Commenting.MessageTemplates;

namespace MrCMS.Web.Apps.Commenting.Services
{
    public class QueueCommentAddedNotificationEmails : IOnAdded<Comment>
    {
        private readonly IMessageParser<CommentAddedMessageTemplate, Comment> _messageParser;

        public QueueCommentAddedNotificationEmails(IMessageParser<CommentAddedMessageTemplate, Comment> messageParser)
        {
            _messageParser = messageParser;
        }

        public void Execute(OnAddedArgs<Comment> args)
        {
            var queuedMessage = _messageParser.GetMessage(args.Item);
            _messageParser.QueueMessage(queuedMessage);
        }
    }
}