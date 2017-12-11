using System.Web.Mvc;
using MrCMS.Web.Apps.Commenting.Areas.Admin.Models;
using MrCMS.Web.Apps.Commenting.Areas.Admin.Services;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Website.Controllers;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.Controllers
{
    public class ReportedCommentController : MrCMSAppAdminController<CommentingApp>
    {
        private readonly IReportedCommentAdminService _adminService;

        public ReportedCommentController(IReportedCommentAdminService adminService)
        {
            _adminService = adminService;
        }

        public ViewResult Index(ReportedCommentSearchQuery query)
        {
            ViewData["results"] = _adminService.Search(query);

            return View(query);
        }

        public ViewResult Show(ReportedComment reportedComment)
        {
            return View(reportedComment);
        }
    }
}