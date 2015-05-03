using System.Web.Mvc;
using MrCMS.Web.Apps.Commenting.Areas.Admin.ModelBinders;
using MrCMS.Web.Apps.Commenting.Areas.Admin.Services;
using MrCMS.Web.Apps.Commenting.Settings;
using MrCMS.Web.Areas.Admin.Helpers;
using MrCMS.Website.Binders;
using MrCMS.Website.Controllers;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.Controllers
{
    public class CommentingSettingsController : MrCMSAppAdminController<CommentingApp>
    {
        private readonly ICommentingSettingsAdminService _commentingSettingsAdminService;

        public CommentingSettingsController(ICommentingSettingsAdminService commentingSettingsAdminService)
        {
            _commentingSettingsAdminService = commentingSettingsAdminService;
        }

        [HttpGet]
        public ViewResult Index()
        {
            ViewData["page-types"] = _commentingSettingsAdminService.GetAllPageTypes();
            ViewData["comment-approval-types"] = _commentingSettingsAdminService.GetCommentApprovalTypes();
            return View(_commentingSettingsAdminService.GetSettings());
        }

        [HttpPost]
        public RedirectToRouteResult Index([IoCModelBinder(typeof(CommentingSettingsModelBinder))]CommentingSettings newSettings)
        {
            _commentingSettingsAdminService.UpdateSettings(newSettings);
            TempData.SuccessMessages().Add("Commenting settings saved");
            return RedirectToAction("Index");
        }
    }
}