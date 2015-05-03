using MrCMS.Web.Apps.Commenting.Areas.Admin.ModelBinders;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Website;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.Services
{
    public class UpdateCommentingInfo : EndRequestTask<UpdateCommentingInfoData>
    {
        public UpdateCommentingInfo(UpdateCommentingInfoData data) : base(data)
        {
        }
    }
}