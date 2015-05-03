using MrCMS.Entities.People;
using MrCMS.Helpers;
using MrCMS.Web.Apps.Commenting.Entities.UserProfile;

namespace MrCMS.Web.Apps.Commenting.Helpers
{
    public static class UserExtensions
    {
        public static CommentsInfo GetCommentsInfo(this User user)
        {
            return user.Get<CommentsInfo>() ?? new CommentsInfo {User = user};
        }
    }
}