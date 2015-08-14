using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MrCMS.Helpers.Validation;

namespace MrCMS.Web.Apps.Commenting.Models
{
    public interface IHaveIPAddress
    {
        string IPAddress { get; set; }
    }

    public interface IAddCommentModel : IHaveIPAddress
    {
        int WebpageId { get; set; }
        int? InReplyTo { get; set; }
        bool ReplyNotification { get; set; }

        [Required]
        string Message { get; set; }
    }

    public class GuestAddCommentModel : IAddCommentModel
    {
        public GuestAddCommentModel()
        {
            ReplyNotification = true;
        }

        public int WebpageId { get; set; }
        public int? InReplyTo { get; set; }
        [DisplayName("Notify me when people reply")]
        public bool ReplyNotification { get; set; }

        [Required]
        public string Message { get; set; }

        public string IPAddress { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailValidator]
        [DisplayName("Email (will not be shown)")]
        public string Email { get; set; }
    }
}