using MrCMS.Settings;

namespace MrCMS.Web.Apps.Commenting.Settings
{
    public class GoogleReCaptchaSettings : SiteSettingsBase
    {
        public override bool RenderInSettings
        {
            get { return true; }
        }

        public bool Enabled { get; set; }
        public string Secret { get; set; }
        public string SiteKey { get; set; }
    }
}