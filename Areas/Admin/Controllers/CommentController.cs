using System.Web.Mvc;
using MrCMS.Web.Apps.Commenting.Areas.Admin.ModelBinders;
using MrCMS.Web.Apps.Commenting.Areas.Admin.Models;
using MrCMS.Web.Apps.Commenting.Areas.Admin.Services;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Website.Binders;
using MrCMS.Website.Controllers;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.Controllers
{
    public class CommentController : MrCMSAppAdminController<CommentingApp>
    {
        private readonly ICommentAdminService _commentAdminService;

        public CommentController(ICommentAdminService commentAdminService)
        {
            _commentAdminService = commentAdminService;
        }

        public ViewResult Index([IoCModelBinder(typeof(CommentSearchQueryModelBinder))]CommentSearchQuery searchQuery)
        {
            ViewData["approval-options"] = _commentAdminService.GetApprovalOptions();
            ViewData["results"] = _commentAdminService.Search(searchQuery);
            return View(searchQuery);
        }

        public ViewResult Show(Comment comment)
        {
            return View(comment);
        }

        [HttpPost]
        public RedirectToRouteResult Approval([IoCModelBinder(typeof(CommentApprovalModelBinder))]Comment comment)
        {
            _commentAdminService.Update(comment);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult Delete(Comment comment)
        {
            return View(comment);
        }
        [HttpPost]
        [ActionName("Delete")]
        public RedirectToRouteResult Delete_POST(Comment comment)
        {
            _commentAdminService.Delete(comment);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ViewResult Edit(Comment comment)
        {
            return View(comment);
        }

        [HttpPost, ActionName("Edit")]
        public RedirectToRouteResult Edit_POST(Comment comment)
        {
            _commentAdminService.Update(comment);
            return RedirectToAction("Index");
        }
    }
}