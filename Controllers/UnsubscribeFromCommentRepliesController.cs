using System;
using System.Web.Mvc;
using MrCMS.Web.Apps.Commenting.Pages;
using MrCMS.Web.Apps.Commenting.Services;
using MrCMS.Website.Controllers;

namespace MrCMS.Web.Apps.Commenting.Controllers
{
    public class UnsubscribeFromCommentRepliesController : MrCMSAppUIController<CommentingApp>
    {
        private readonly IUnsubscribeFromCommentNotifications _unsubscribeFromCommentNotifications;
        private readonly IGetCommentByGuid _getCommentByGuid;

        public UnsubscribeFromCommentRepliesController(IUnsubscribeFromCommentNotifications unsubscribeFromCommentNotifications, IGetCommentByGuid getCommentByGuid)
        {
            _unsubscribeFromCommentNotifications = unsubscribeFromCommentNotifications;
            _getCommentByGuid = getCommentByGuid;
        }

        public ActionResult Show(UnsubscribeFromCommentReplies page, Guid guid)
        {
            var comment = _getCommentByGuid.Get(guid);
            if (comment != null)
            {
                _unsubscribeFromCommentNotifications.Unsubscribe(comment);
                return View(page);
            }
            return Redirect("~/");
        }
    }
}