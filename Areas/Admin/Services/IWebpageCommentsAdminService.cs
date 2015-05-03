using System.Collections;
using System.Collections.Generic;
using MrCMS.Entities.Documents.Web;
using MrCMS.Web.Apps.Commenting.Entities;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.Services
{
    public interface IWebpageCommentsAdminService   
    {
        CommentingInfo GetCommentingInfo(Webpage webpage);
        IList<Comment> GetComments(Webpage webpage);
    }
}