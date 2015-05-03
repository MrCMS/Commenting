using System.Web.Mvc;

namespace MrCMS.Web.Apps.Commenting.Models
{
    public class CommentingUserAccountModel
    {
        public int Id { get; set; }

        [Remote("IsUniqueUsername", "CommentingUserAccount", AdditionalFields = "Id")]
        public string Username { get; set; }


        public string Message { get; set; }
    }
}