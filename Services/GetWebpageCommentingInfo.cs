using MrCMS.Entities.Documents.Web;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Website;
using NHibernate;

namespace MrCMS.Web.Apps.Commenting.Services
{
    public class GetWebpageCommentingInfo : IGetWebpageCommentingInfo
    {
        private readonly ISession _session;

        public GetWebpageCommentingInfo(ISession session)
        {
            _session = session;
        }

        public CommentingInfo Get(Webpage webpage)
        {
            if (webpage == null)
                return null;

            if (CurrentRequestData.CurrentSite == null)
                return null;
            return
                _session.QueryOver<CommentingInfo>()
                    .Where(info => info.Webpage.Id == webpage.Id && CurrentRequestData.CurrentSite.Id == info.Site.Id)
                    .Cacheable()
                    .SingleOrDefault();
        }
    }
}