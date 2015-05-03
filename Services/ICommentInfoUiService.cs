using MrCMS.Web.Apps.Commenting.Models;

namespace MrCMS.Web.Apps.Commenting.Services
{
    public interface ICommentInfoUiService
    {
        void Save(CommentingUserAccountModel model);
    }
}