using MrCMS.Web.Apps.Commenting.Models;

namespace MrCMS.Web.Apps.Commenting.Helpers
{
    public static class CommentResponseInfoExtensions
    {
        public static bool IsSuccess(this ICommentResponseInfo commentResponseInfo)
        {
            return commentResponseInfo != null && commentResponseInfo.Type == CommentResponseType.Success;
        }
    }
}