using System.ComponentModel;
using MrCMS.Entities;
using MrCMS.Entities.Documents.Web;

namespace MrCMS.Web.Apps.Commenting.Entities
{
    public class CommentingInfo : SiteEntity
    {
        [DisplayName("Enabled Status")]
        public virtual CommentingEnabledStatus CommentingEnabledStatus { get; set; }

        public virtual Webpage Webpage { get; set; }
    }
}