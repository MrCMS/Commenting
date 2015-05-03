using MrCMS.Web.Apps.Commenting.Entities;

namespace MrCMS.Web.Apps.Commenting.Events
{
    public class CommentReportedEventArgs
    {
        public CommentReportedEventArgs(Comment comment)
        {
            Comment = comment;
        }

        public Comment Comment { get; set; }
    }
}