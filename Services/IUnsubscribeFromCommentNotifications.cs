using MrCMS.Web.Apps.Commenting.Entities;

namespace MrCMS.Web.Apps.Commenting.Services
{
    public interface IUnsubscribeFromCommentNotifications
    {
        void Unsubscribe(Comment comment);
    }
}