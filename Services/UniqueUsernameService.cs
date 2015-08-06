using MrCMS.Helpers;
using MrCMS.Web.Apps.Commenting.Entities.UserProfile;
using NHibernate;

namespace MrCMS.Web.Apps.Commenting.Services
{
    public class UniqueUsernameService : IUniqueUsernameService
    {
        private readonly ISession _session;

        public UniqueUsernameService(ISession session)
        {
            _session = session;
        }

        /// <summary> 
        /// Checks to see if the supplied username is unique
        /// </summary>
        /// <param name="username"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsUniqueUsername(string username, int? id = null)
        {
            var queryOver = _session.QueryOver<CommentsInfo>().Where(x => x.Username == username);
            if (id.HasValue)
            {
                queryOver = queryOver.Where(x => x.Id != id.Value);
            }
            return queryOver.Any();
        }
    }
}