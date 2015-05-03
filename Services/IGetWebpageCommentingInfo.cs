using MrCMS.Entities.Documents.Web;
using MrCMS.Web.Apps.Commenting.Entities;

namespace MrCMS.Web.Apps.Commenting.Services
{
    public interface IGetWebpageCommentingInfo
    {
        CommentingInfo Get(Webpage webpage);
    }
}