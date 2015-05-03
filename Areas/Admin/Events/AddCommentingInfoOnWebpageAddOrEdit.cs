using MrCMS.Entities.Documents.Web;
using MrCMS.Events;
using MrCMS.Web.Apps.Commenting.Services;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.Events
{
    public class AddCommentingInfoOnWebpageAddOrEdit : IOnAdded<Webpage>, IOnUpdated<Webpage>
    {
        private readonly IAddCommentingInfoForWebpage _addCommentingInfoForWebpage;

        public AddCommentingInfoOnWebpageAddOrEdit(IAddCommentingInfoForWebpage addCommentingInfoForWebpage)
        {
            _addCommentingInfoForWebpage = addCommentingInfoForWebpage;
        }

        public void Execute(OnAddedArgs<Webpage> args)
        {
            _addCommentingInfoForWebpage.AddInfo(args.Item);
        }

        public void Execute(OnUpdatedArgs<Webpage> args)
        {
            _addCommentingInfoForWebpage.AddInfo(args.Item);
        }
    }
}