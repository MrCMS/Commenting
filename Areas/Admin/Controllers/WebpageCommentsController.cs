using System.Web.Mvc;
using MrCMS.Entities.Documents.Web;
using MrCMS.Web.Apps.Commenting.Areas.Admin.Services;
using MrCMS.Website.Controllers;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.Controllers
{
    public class WebpageCommentsController : MrCMSAppAdminController<CommentingApp>
    {
        private readonly IWebpageCommentsAdminService _webpageCommentsAdminService;

        public WebpageCommentsController(IWebpageCommentsAdminService webpageCommentsAdminService)
        {
            _webpageCommentsAdminService = webpageCommentsAdminService;
        }

        public PartialViewResult Show(Webpage webpage)
        {
            ViewData["webpage"] = webpage;
            ViewData["comments"] = _webpageCommentsAdminService.GetComments(webpage);
            return PartialView(_webpageCommentsAdminService.GetCommentingInfo(webpage));
        }
    }
}