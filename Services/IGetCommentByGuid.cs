using System;
using MrCMS.Web.Apps.Commenting.Entities;
using NHibernate;

namespace MrCMS.Web.Apps.Commenting.Services
{
    public interface IGetCommentByGuid
    {
        Comment Get(Guid guid);
    }

    public class GetCommentByGuid : IGetCommentByGuid
    {
        private readonly ISession _session;

        public GetCommentByGuid(ISession session)
        {
            _session = session;
        }

        public Comment Get(Guid guid)
        {
            return
                _session.QueryOver<Comment>()
                    .Where(comment => comment.Guid == guid)
                    .Take(1)
                    .Cacheable()
                    .SingleOrDefault();
        }
    }
}