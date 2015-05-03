using System.Linq;
using MrCMS.Entities.Documents.Web;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Web.Apps.Commenting.Settings;

namespace MrCMS.Web.Apps.Commenting.Helpers
{
    public static class CommentingExtensions
    {
        public static bool AreCommentsEnabled(this Webpage webpage, CommentingInfo commentingInfo, CommentingSettings settings)
        {
            if (webpage == null || commentingInfo == null || settings == null)
                return false;

            if (commentingInfo.CommentingEnabledStatus != CommentingEnabledStatus.System)
            {
                return commentingInfo.CommentingEnabledStatus == CommentingEnabledStatus.Enabled;
            }

            return settings.AllowedTypes.Contains(webpage.GetType());
        }
    }
}