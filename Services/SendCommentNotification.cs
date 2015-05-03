using System;
using MrCMS.Helpers;
using MrCMS.Services;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Web.Apps.Commenting.MessageTemplates;
using NHibernate;

namespace MrCMS.Web.Apps.Commenting.Services
{
    public class SendCommentNotification : ISendCommentNotification
    {
        private readonly IMessageParser<NewCommentReplyMessageTemplate, Comment> _messageParser;
        private readonly ISession _session;

        public SendCommentNotification(IMessageParser<NewCommentReplyMessageTemplate, Comment> messageParser, 
            ISession session)
        {
            _messageParser = messageParser;
            _session = session;
        }

        public void Send(Comment comment)
        {
            if (Convert.ToBoolean(comment.Approved))
            {
                // Comment needs to be a 'reply' to another comment.
                if (comment.InReplyTo != null)
                {
                    // Parent Comment needs to have notifications enabled.
                    if (comment.InReplyTo.SendReplyNotifications)
                    {
                        // Notifcation must not have already been sent.
                        if (!comment.ParentNotificationSent)
                        {
                            // Send
                            var queuedMessage = _messageParser.GetMessage(comment);
                            if (queuedMessage != null)
                            {
                                _messageParser.QueueMessage(queuedMessage);

                                comment.ParentNotificationSent = true;
                                _session.Transact(session => session.Update(comment));
                            }
                        }
                    }
                }
            }
        }
    }
}