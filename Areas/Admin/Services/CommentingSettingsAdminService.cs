using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MrCMS.Apps;
using MrCMS.Entities.Documents.Web;
using MrCMS.Helpers;
using MrCMS.Settings;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Web.Apps.Commenting.Settings;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.Services
{
    public class CommentingSettingsAdminService : ICommentingSettingsAdminService
    {
        private readonly IConfigurationProvider _configurationProvider;

        public CommentingSettingsAdminService(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public List<Type> GetAllPageTypes()
        {
            return
                TypeHelper.GetAllConcreteMappedClassesAssignableFrom<Webpage>()
                    .Where(type => MrCMSApp.AppWebpages.ContainsKey(type))
                    .OrderBy(type => type.FullName).ToList();
        }

        public void UpdateSettings(CommentingSettings settings)
        {
            _configurationProvider.SaveSettings(settings);
        }

        public CommentingSettings GetSettings()
        {
            return _configurationProvider.GetSiteSettings<CommentingSettings>();
        }

        public List<SelectListItem> GetCommentApprovalTypes()
        {
            return
                Enum.GetValues(typeof(CommentApprovalType))
                    .Cast<CommentApprovalType>()
                    .BuildSelectItemList(type => type.ToString(), emptyItem: null);
        }
    }
}