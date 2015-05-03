using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MrCMS.Entities.People;

namespace MrCMS.Web.Apps.Commenting.Entities.UserProfile
{
    public class CommentsInfo : UserProfileData
    {
        [Remote("IsUniqueUsername", "CommentsInfo", AdditionalFields = "Id")]
        public virtual string Username { get; set; }
    }
}