namespace MrCMS.Web.Apps.Commenting.Models
{
    public class ReportModel : IHaveIPAddress
    {
        public int CommentId { get; set; }
        public string IPAddress { get; set; }
    }
}