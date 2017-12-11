using System.Collections.Generic;
using System.Web.Mvc;
using MrCMS.Models;
using MrCMS.Web.Apps.Commenting.ACL;
using MrCMS.Website;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.Models
{
    public class CommentingAdminMenuModel : IAdminMenuItem
    {
        private readonly UrlHelper _urlHelper;

        public CommentingAdminMenuModel(UrlHelper urlHelper)
        {
            _urlHelper = urlHelper;
        }

        private SubMenu _children;
        private readonly CommentingAdminACL _commentingAdminACL = new CommentingAdminACL();
        public string Text { get { return "Comments"; } }

        public string IconClass
        {
            get { return "fa fa-comment-o"; }
        }

        public string Url { get; private set; }
        public bool CanShow
        {
            get
            {
                return _commentingAdminACL.CanAccess(CurrentRequestData.CurrentUser, CommentingAdminACL.ShowMenu);
            }
        }
        private SubMenu GetChildren()
        {
            return new SubMenu
            {
                new ChildMenuItem("Comments", _urlHelper.Action("Index", "Comment"),
                    ACLOption.Create(_commentingAdminACL, CommentingAdminACL.ViewComments)),
                new ChildMenuItem("Reported Comments", _urlHelper.Action("Index", "ReportedComment"),
                    ACLOption.Create(_commentingAdminACL, CommentingAdminACL.ViewComments)),
                new ChildMenuItem("Settings", _urlHelper.Action("Index", "CommentingSettings"),
                    ACLOption.Create(_commentingAdminACL, CommentingAdminACL.EditSettings))
            };
        }
        public SubMenu Children { get { return _children ?? (_children = GetChildren()); } }


        public int DisplayOrder { get { return 90; } }
    }
}