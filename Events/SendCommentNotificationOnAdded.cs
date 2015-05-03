using MrCMS.Events;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Web.Apps.Commenting.Services;

namespace MrCMS.Web.Apps.Commenting.Events
{
    public class SendCommentNotificationOnAdded : IOnAdded<Comment>
    {
        private readonly ISendCommentNotification _sendCommentNotification;

        public SendCommentNotificationOnAdded(ISendCommentNotification sendCommentNotification)
        {
            _sendCommentNotification = sendCommentNotification;
        }

        public void Execute(OnAddedArgs<Comment> args)
        {
            var comment = args.Item;
            _sendCommentNotification.Send(comment);
        }
    }
}