using System.Collections.Generic;
using System.Web.Mvc;
using MrCMS.Paging;
using MrCMS.Web.Apps.Commenting.Areas.Admin.Models;
using MrCMS.Web.Apps.Commenting.Entities;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.Services
{
    public interface ICommentAdminService
    {
        IPagedList<Comment> Search(CommentSearchQuery query);
        List<SelectListItem> GetApprovalOptions();
        void Update(Comment comment);
        void Delete(Comment comment);
    }
}