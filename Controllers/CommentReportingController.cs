using System.Web.Mvc;
using MrCMS.Web.Apps.Commenting.ModelBinders;
using MrCMS.Web.Apps.Commenting.Models;
using MrCMS.Web.Apps.Commenting.Services;
using MrCMS.Website.Binders;

namespace MrCMS.Web.Apps.Commenting.Controllers
{
    public class CommentReportingController : BaseCommentUiController
    {
        private readonly ICommentReportingUiService _commentReportingUiService;

        public CommentReportingController(ICommentReportingUiService commentReportingUiService)
        {
            _commentReportingUiService = commentReportingUiService;
        }

        [HttpPost]
        public ActionResult Report([IoCModelBinder(typeof(SetIPAddressModelBinder))]ReportModel reportModel)
        {
            var response = _commentReportingUiService.Report(reportModel);
            return RedirectToPage(response);
        }
    }
}