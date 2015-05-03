using MrCMS.Events;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Web.Apps.Commenting.Services;

namespace MrCMS.Web.Apps.Commenting.Events
{
    public class SendCommentNotificationOnUpdated : IOnUpdated<Comment>
    {
        private readonly ISendCommentNotification _sendCommentNotification;

        public SendCommentNotificationOnUpdated(ISendCommentNotification sendCommentNotification)
        {
            _sendCommentNotification = sendCommentNotification;
        }

        public void Execute(OnUpdatedArgs<Comment> args)
        {
            var comment = args.Item;
            _sendCommentNotification.Send(comment);
        }
    }
}