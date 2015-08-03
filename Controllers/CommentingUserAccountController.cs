using System.Web.Mvc;
using MrCMS.Services;
using MrCMS.Services.Resources;
using MrCMS.Web.Apps.Commenting.Helpers;
using MrCMS.Web.Apps.Commenting.Models;
using MrCMS.Web.Apps.Commenting.Services;
using MrCMS.Web.Apps.Core.Pages;
using MrCMS.Website;

namespace MrCMS.Web.Apps.Commenting.Controllers
{
    public class CommentingUserAccountController : BaseCommentUiController
    {
        private readonly IUniqueUsernameService _uniqueUsernameService;
        private readonly IUniquePageService _uniquePageService;
        private readonly ICommentInfoUiService _commentInfoUiService;
        private readonly IStringResourceProvider _stringResourceProvider;

        public CommentingUserAccountController(IUniqueUsernameService uniqueUsernameService, IUniquePageService uniquePageService, ICommentInfoUiService commentInfoUiService, IStringResourceProvider stringResourceProvider)
        {
            _uniqueUsernameService = uniqueUsernameService;
            _uniquePageService = uniquePageService;
            _commentInfoUiService = commentInfoUiService;
            _stringResourceProvider = stringResourceProvider;
        }

        [HttpGet]
        public ActionResult Username()
        {
            var user = CurrentRequestData.CurrentUser;
            var commentsUserInfo = user.GetCommentsInfo();

            if (!string.IsNullOrWhiteSpace(commentsUserInfo.Username))
            {
                var model = new CommentingUserAccountModel
                {
                    Id = commentsUserInfo.Id,
                    Username = commentsUserInfo.Username
                };
                ModelState.Clear();
                return View(model);
            }
            return View(new CommentingUserAccountModel());
        }

        [HttpPost]
        public ActionResult UpdateUsername(CommentingUserAccountModel model)
        {
            if (ModelState.IsValid)
            {
                _commentInfoUiService.Save(model);
                model.Message = _stringResourceProvider.GetValue("Commenting App - Username successfully updated.", "Username successfully updated.");
            }
            else
            {
                model.Message = _stringResourceProvider.GetValue("Commenting App - Please ensure to fill out the usename.", "Please ensure to fill out the usename.");
            }

            TempData["message"] = model.Message;
            return _uniquePageService.RedirectTo<UserAccountPage>();
        }

        public JsonResult IsUniqueUsername(string commentingUsername, int? id)
        {
            if (_uniqueUsernameService.IsUniqueUsername(commentingUsername, id))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }

            return Json(_stringResourceProvider.GetValue("Commenting App - Username already registered.", "Username already registered."), JsonRequestBehavior.AllowGet);
        }
    }
}
