using System;
using System.Linq;
using System.Web.Mvc;
using MrCMS.Entities.Documents.Web;
using MrCMS.Services;
using MrCMS.Web.Apps.Commenting.Areas.Admin.Services;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Web.Apps.Commenting.Services;
using MrCMS.Website;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.ModelBinders
{
    public class WebpageCommentingInfoModelBinder : CustomBinderBase<Webpage>
    {
        private const string EnabledStatusKey = "CommentingEnabledStatus";
        private readonly IGetWebpageCommentingInfo _getWebpageCommentingInfo;

        public WebpageCommentingInfoModelBinder(IGetWebpageCommentingInfo getWebpageCommentingInfo)
        {
            _getWebpageCommentingInfo = getWebpageCommentingInfo;
        }

        public override void ApplyCustomBinding(Webpage entity, ControllerContext controllerContext)
        {
            var nameValueCollection = controllerContext.HttpContext.Request.Form;
            if (!nameValueCollection.AllKeys.Contains(EnabledStatusKey))
                return;

            var status = nameValueCollection[EnabledStatusKey];
            CommentingEnabledStatus enabledStatus;
            if (!Enum.TryParse(status, out enabledStatus))
                return;

            CurrentRequestData.OnEndRequest.Add(new UpdateCommentingInfo(new UpdateCommentingInfoData
            {
                Status = enabledStatus,
                WebpageId = entity.Id
            }));
        }
    }

    public class UpdateCommentingInfoData
    {
        public CommentingEnabledStatus Status { get; set; }
        public int WebpageId { get; set; }
    }
}