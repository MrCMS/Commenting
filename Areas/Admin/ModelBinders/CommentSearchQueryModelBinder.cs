using System;
using System.Web;
using System.Web.Mvc;
using MrCMS.Web.Apps.Commenting.Areas.Admin.Models;
using MrCMS.Website.Binders;
using Ninject;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.ModelBinders
{
    public class CommentSearchQueryModelBinder : MrCMSDefaultModelBinder
    {
        private const string CommentQueryKey = "admin.commentquery";
        private readonly HttpSessionStateBase _sessionStateBase;

        public CommentSearchQueryModelBinder(IKernel kernel, HttpSessionStateBase sessionStateBase)
            : base(kernel)
        {
            _sessionStateBase = sessionStateBase;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object bindModel = base.BindModel(controllerContext, bindingContext);
            if (bindModel is CommentSearchQuery)
                _sessionStateBase[CommentQueryKey] = bindModel;
            return bindModel;
        }

        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext,
            Type modelType)
        {
            if (modelType != typeof (CommentSearchQuery))
                return base.CreateModel(controllerContext, bindingContext, modelType);
            CommentSearchQuery commentSearchQuery = !controllerContext.HttpContext.Request.QueryString.HasKeys()
                ? _sessionStateBase[CommentQueryKey] as CommentSearchQuery ?? new CommentSearchQuery()
                : new CommentSearchQuery();
            commentSearchQuery.Page = 1;
            return commentSearchQuery;
        }
    }
}