using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MrCMS.Web.Apps.Commenting.Models
{
    public class LoggedInUserAddCommentModel : IAddCommentModel
    {
        public int WebpageId { get; set; }

        public int? InReplyTo { get; set; }
        [DisplayName("Notify me when people reply")]
        public bool ReplyNotification { get; set; }

        [Required]
        public string Message { get; set; }

        public string IPAddress { get; set; }
    }
}