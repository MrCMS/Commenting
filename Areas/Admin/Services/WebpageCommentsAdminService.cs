using System.Collections.Generic;
using System.Linq;
using MrCMS.Entities.Documents.Web;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Web.Apps.Commenting.Services;
using NHibernate;
using NHibernate.Linq;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.Services
{
    public class WebpageCommentsAdminService : IWebpageCommentsAdminService
    {
        private readonly IGetWebpageCommentingInfo _getWebpageCommentingInfo;
        private readonly ISession _session;

        public WebpageCommentsAdminService(IGetWebpageCommentingInfo getWebpageCommentingInfo,ISession session)
        {
            _getWebpageCommentingInfo = getWebpageCommentingInfo;
            _session = session;
        }

        public CommentingInfo GetCommentingInfo(Webpage webpage)
        {
            return _getWebpageCommentingInfo.Get(webpage);
        }

        public IList<Comment> GetComments(Webpage webpage)
        {
            return _session.Query<Comment>().Where(comment => comment.Webpage.Id == webpage.Id).ToList();
        }
    }
}