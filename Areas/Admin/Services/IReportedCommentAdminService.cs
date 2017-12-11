using MrCMS.Paging;
using MrCMS.Web.Apps.Commenting.Areas.Admin.Controllers;
using MrCMS.Web.Apps.Commenting.Areas.Admin.Models;
using MrCMS.Web.Apps.Commenting.Entities;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.Services
{
    public interface IReportedCommentAdminService
    {
        IPagedList<ReportedComment> Search(ReportedCommentSearchQuery query);
    }
}