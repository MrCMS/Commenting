using MrCMS.Helpers;
using MrCMS.Services;
using MrCMS.Web.Apps.Commenting.Entities.UserProfile;
using MrCMS.Web.Apps.Commenting.Models;
using MrCMS.Website;
using NHibernate;

namespace MrCMS.Web.Apps.Commenting.Services
{
    public class CommentInfoUiService : ICommentInfoUiService
    {
        private readonly ISession _session;
        private readonly IUserProfileDataService _userProfileDataService;

        public CommentInfoUiService(ISession session, IUserProfileDataService userProfileDataService)
        {
            _session = session;
            _userProfileDataService = userProfileDataService;
        }

        public void Save(CommentingUserAccountModel model)
        {
            var currentUser = CurrentRequestData.CurrentUser;
            CommentsInfo commentsInfo;
            if (currentUser.Get<CommentsInfo>() != null)
            {
                commentsInfo = currentUser.Get<CommentsInfo>();
            }
            else
            {
                commentsInfo = new CommentsInfo{User = currentUser};
                _userProfileDataService.Add(commentsInfo);
            }

            _session.Transact(session =>
            {
                commentsInfo.Username = model.Username;
                session.SaveOrUpdate(commentsInfo);
            });
        }
    }
}