namespace MrCMS.Web.Apps.Commenting.Areas.Admin.Models
{
    public class ReportedCommentSearchQuery
    {
        public ReportedCommentSearchQuery()
        {
            Page = 1;
        }
        public int Page { get; set; }
        public string SearchTerm { get; set; }
    }
}