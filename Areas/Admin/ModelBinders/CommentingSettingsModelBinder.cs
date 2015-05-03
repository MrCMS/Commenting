using System;
using System.Collections.Generic;
using System.Linq;
using MrCMS.Helpers;
using MrCMS.Web.Apps.Commenting.Settings;
using MrCMS.Website.Binders;
using Ninject;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.ModelBinders
{
    public class CommentingSettingsModelBinder : MrCMSDefaultModelBinder
    {
        public const string PageType = "page-type-";

        public CommentingSettingsModelBinder(IKernel kernel) : base(kernel)
        {

        }

        public override object BindModel(System.Web.Mvc.ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            var bindModel = base.BindModel(controllerContext, bindingContext);
            var commentingSettings = bindModel as CommentingSettings;
            if (commentingSettings == null)
                return bindModel;
            var types = new List<Type>();
            foreach (var source in controllerContext.HttpContext.Request.Params.AllKeys.Where(s => s.StartsWith(PageType)))
            {
                var value = controllerContext.HttpContext.Request[source];
                if (value.Contains("true"))
                    types.Add(TypeHelper.GetTypeByName(source.Replace(PageType, string.Empty)));
            }
            commentingSettings.SetAllowedPageTypes(types.ToArray());
            return commentingSettings;
        }

        protected override object CreateModel(System.Web.Mvc.ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext, Type modelType)
        {
            return Kernel.Get<CommentingSettings>();
        }
    }
}