using MrCMS.Helpers;
using MrCMS.Web.Apps.Commenting.Entities;
using NHibernate;

namespace MrCMS.Web.Apps.Commenting.Services
{
    public class UnsubscribeFromCommentNotifications : IUnsubscribeFromCommentNotifications
    {
        private readonly ISession _session;

        public UnsubscribeFromCommentNotifications(ISession session)
        {
            _session = session;
        }

        public void Unsubscribe(Comment comment)
        {
            comment.SendReplyNotifications = false;
            _session.Transact(session => session.Update(comment));
        }
    }
}