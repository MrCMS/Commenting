using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MrCMS.Helpers;
using MrCMS.Web.Apps.Commenting.Settings;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.Services
{
    public interface ICommentingSettingsAdminService
    {
        List<Type> GetAllPageTypes();
        void UpdateSettings(CommentingSettings settings);
        CommentingSettings GetSettings();
        List<SelectListItem> GetCommentApprovalTypes();
    }
}