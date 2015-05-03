using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MrCMS.Entities.Documents.Web;
using MrCMS.Web.Areas.Admin.Models.WebpageEdit;

namespace MrCMS.Web.Apps.Commenting.Areas.Admin.Models
{
    public class CommentingWebpageTab : WebpageTab
    {
        public override int Order
        {
            get { return 1000; }
        }

        public override string Name(Webpage webpage)
        {
            return "Comments";
        }

        public override bool ShouldShow(Webpage webpage)
        {
            return true;
        }

        public override Type ParentType
        {
            get { return null; }
        }

        public override string TabHtmlId
        {
            get { return "webpage-comments"; }
        }

        public override void RenderTabPane(HtmlHelper<Webpage> html, Webpage webpage)
        {
            html.RenderAction("Show", "WebpageComments", new { webpage });
        }
    }
}