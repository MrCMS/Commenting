using System.Collections.Generic;
using MrCMS.Services;

namespace MrCMS.Web.Apps.Commenting.Services
{
    public class CommentingStylesheets : IAppStylesheetList
    {
        public IEnumerable<string> UIStylesheets
        {
            //get { yield return "~/Apps/Commenting/Content/Styles/commenting.css"; }
            get { yield break; }
        }

        public IEnumerable<string> AdminStylesheets
        {
            get { yield break; }
        }
    }
    public class CommentingScripts : IAppScriptList
    {
        public IEnumerable<string> UIScripts
        {
            get { yield return "~/Apps/Commenting/Content/Scripts/ui.js"; }
        }

        public IEnumerable<string> AdminScripts
        {
            get { yield break; }
        }
    }
}