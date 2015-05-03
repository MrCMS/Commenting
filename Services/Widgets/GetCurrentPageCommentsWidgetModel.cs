using MrCMS.Entities.Documents.Web;
using MrCMS.Services.Widgets;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Web.Apps.Commenting.Helpers;
using MrCMS.Web.Apps.Commenting.Settings;
using MrCMS.Web.Apps.Commenting.Widgets;
using MrCMS.Website;

namespace MrCMS.Web.Apps.Commenting.Services.Widgets
{
    public class GetCurrentPageCommentsWidgetModel : GetWidgetModelBase<CurrentPageCommentsWidget>
    {
        private readonly CommentingSettings _commentingSettings;
        private readonly IGetWebpageCommentingInfo _getWebpageCommentingInfo;

        public GetCurrentPageCommentsWidgetModel(IGetWebpageCommentingInfo getWebpageCommentingInfo,
            CommentingSettings commentingSettings)
        {
            _getWebpageCommentingInfo = getWebpageCommentingInfo;
            _commentingSettings = commentingSettings;
        }

        public override object GetModel(CurrentPageCommentsWidget widget)
        {
            Webpage currentPage = CurrentRequestData.CurrentPage;
            if (currentPage == null)
                return null;
            CommentingInfo commentingInfo = _getWebpageCommentingInfo.Get(currentPage);
            if (!currentPage.AreCommentsEnabled(commentingInfo, _commentingSettings))
                return null;
            return currentPage;
        }
    }
}