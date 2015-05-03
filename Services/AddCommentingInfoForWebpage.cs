using MrCMS.Entities.Documents.Web;
using MrCMS.Helpers;
using MrCMS.Web.Apps.Commenting.Entities;
using NHibernate;

namespace MrCMS.Web.Apps.Commenting.Services
{
    public class AddCommentingInfoForWebpage : IAddCommentingInfoForWebpage
    {
        private readonly ISession _session;

        public AddCommentingInfoForWebpage(ISession session)
        {
            _session = session;
        }

        public void AddInfo(Webpage webpage)
        {
            if (_session.QueryOver<CommentingInfo>().Where(info => info.Webpage.Id == webpage.Id).Cacheable().Any())
            {
                return;
            }

            _session.Transact(session => session.Save(new CommentingInfo
            {
                Webpage = webpage,
                CommentingEnabledStatus = CommentingEnabledStatus.System
            }));
        }
    }
}