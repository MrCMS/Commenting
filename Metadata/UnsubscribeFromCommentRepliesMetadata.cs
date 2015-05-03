using MrCMS.Entities.Documents.Metadata;
using MrCMS.Web.Apps.Commenting.Pages;

namespace MrCMS.Web.Apps.Commenting.Metadata
{
    public class UnsubscribeFromCommentRepliesMetadata : DocumentMetadataMap<UnsubscribeFromCommentReplies>
    {
        public override bool HasBodyContent
        {
            get { return false; }
        }

        public override bool RevealInNavigation
        {
            get { return false; }
        }

        public override string WebGetController
        {
            get { return "UnsubscribeFromCommentReplies"; }
        }
    }
}