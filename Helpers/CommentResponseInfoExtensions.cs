using System.Linq;
using MrCMS.Entities.Documents.Web;
using MrCMS.Web.Apps.Commenting.Entities;
using MrCMS.Web.Apps.Commenting.Models;
using MrCMS.Web.Apps.Commenting.Services;
using MrCMS.Website;
using NHibernate;

namespace MrCMS.Web.Apps.Commenting.Helpers
{
    public static class CommentResponseInfoExtensions
    {
        public static bool IsSuccess(this ICommentResponseInfo commentResponseInfo)
        {
            return commentResponseInfo != null && commentResponseInfo.Type == CommentResponseType.Success;
        }

        public static int GetCommentsCount(this Webpage webpage)
        {
            var commentInfo = MrCMSApplication.Get<ISession>().QueryOver<Comment>()
                .Where(
                    comment =>
                        comment.Webpage == webpage && comment.Approved == true &&
                        comment.InReplyTo == null).List();
            return commentInfo.Count;
        }
    }
}