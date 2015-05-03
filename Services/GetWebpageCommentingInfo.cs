using MrCMS.Entities.Documents.Web;
using MrCMS.Web.Apps.Commenting.Entities;
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
            return
                _session.QueryOver<CommentingInfo>()
                    .Where(info => info.Webpage.Id == webpage.Id)
                    .Cacheable()
                    .SingleOrDefault();
        }
    }
}