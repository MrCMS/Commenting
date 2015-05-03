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
            if (id.HasValue)
            {
                return !_session.QueryOver<CommentsInfo>().Where(x => x.Username == username && x.Id != id.Value).Any();
            }
            return !_session.QueryOver<CommentsInfo>().Where(x => x.Username == username).Any();
        }
    }
}