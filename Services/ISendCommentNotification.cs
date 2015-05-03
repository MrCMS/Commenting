using MrCMS.Web.Apps.Commenting.Entities;

namespace MrCMS.Web.Apps.Commenting.Services
{
    public interface ISendCommentNotification
    {
        void Send(Comment comment);
    }
}