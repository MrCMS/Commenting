using System.Web.Mvc;
using MrCMS.Entities.People;
using MrCMS.Services;
using MrCMS.Web.Apps.Commenting.Entities.UserProfile;
using MrCMS.Web.Apps.Commenting.Services;
using MrCMS.Website.Controllers;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.Controllers
{
    public class CommentsInfoController : MrCMSAppAdminController<CommentingApp>
    {
        private readonly IUserProfileDataService _userProfileDataService;
        private readonly IUniqueUsernameService _uniqueUsernameService;

        public CommentsInfoController(IUserProfileDataService userProfileDataService, IUniqueUsernameService uniqueUsernameService)
        {
            _userProfileDataService = userProfileDataService;
            _uniqueUsernameService = uniqueUsernameService;
        }

        [HttpGet]
        public PartialViewResult Add(User user)
        {
            return PartialView(new CommentsInfo { User = user });
        }

        [HttpPost]
        public RedirectToRouteResult Add(CommentsInfo info)
        {
            _userProfileDataService.Add(info);
            return RedirectToAction("Edit", "User", new { id = info.User.Id });
        }

        [HttpGet]
        public PartialViewResult Edit(CommentsInfo info)
        {
            return PartialView(info);
        }

        [HttpPost]
        [ActionName("Edit")]
        public RedirectToRouteResult Edit_POST(CommentsInfo info)
        {
            _userProfileDataService.Update(info);
            return RedirectToAction("Edit", "User", new { id = info.User.Id });
        }

        [HttpGet]
        public PartialViewResult Delete(CommentsInfo info)
        {
            return PartialView(info);
        }

        [HttpPost]
        [ActionName("Delete")]
        public RedirectToRouteResult Delete_POST(CommentsInfo info)
        {
            _userProfileDataService.Delete(info);
            return RedirectToAction("Edit", "User", new { id = info.User.Id });
        }

        [ChildActionOnly]
        public PartialViewResult Show(CommentsInfo info)
        {
            return PartialView(info);
        }

        public JsonResult IsUniqueUsername(string username, int? id)
        {
            if (_uniqueUsernameService.IsUniqueUsername(username, id))
                return Json(true, JsonRequestBehavior.AllowGet);

            return Json("Username already registered.", JsonRequestBehavior.AllowGet);
        }

    }
}