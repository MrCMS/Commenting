using System.Collections.Generic;
using MrCMS.Entities.Documents.Web;
using MrCMS.Helpers;
using MrCMS.Web.Apps.Commenting.Areas.Admin.ModelBinders;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Web.Apps.Commenting.Services;
using MrCMS.Website;
using NHibernate;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.Services
{
    public class UpdateCommentingExecutor : ExecuteEndRequestBase<UpdateCommentingInfo, UpdateCommentingInfoData>
    {
        private readonly IGetWebpageCommentingInfo _getWebpageCommentingInfo;
        private readonly ISession _session;

        public UpdateCommentingExecutor(IGetWebpageCommentingInfo getWebpageCommentingInfo, ISession session)
        {
            _getWebpageCommentingInfo = getWebpageCommentingInfo;
            _session = session;
        }


        public override void Execute(IEnumerable<UpdateCommentingInfoData> data)
        {
            _session.Transact(session =>
            {
                foreach (var infoData in data)
                {
                    var webpage = session.Get<Webpage>(infoData.WebpageId);
                    var commentingInfo = _getWebpageCommentingInfo.Get(webpage);
                    if (commentingInfo == null)
                        return;
                    commentingInfo.CommentingEnabledStatus = infoData.Status;
                    session.Update(commentingInfo);
                }
            });
        }
    }
}