using System;
using System.Web.Mvc;
using MrCMS.Helpers;
using MrCMS.Web.Apps.Commenting.Settings;
using MrCMS.Website;

namespace MrCMS.Web.Apps.Commenting.Helpers
{
    public static class RecaptchaExtensions
    {
        public static MvcHtmlString RenderRecaptcha(this HtmlHelper helper, string id = null, string errorClass = null, string errorMessage = null)
        {
            var settings = helper.ViewContext.HttpContext.Get<GoogleReCaptchaSettings>();
            if (!settings.Enabled) return MvcHtmlString.Empty;
            var recaptchaDiv = new TagBuilder("div");
            recaptchaDiv.AddCssClass("g-recaptcha");
            recaptchaDiv.Attributes["data-recaptcha-holder"] = "true";
            recaptchaDiv.Attributes["data-sitekey"] = settings.SiteKey;
            id = id ?? Guid.NewGuid().ToString();
            recaptchaDiv.Attributes["id"] = id;

            var message = new TagBuilder("span");
            message.Attributes["data-recaptcha-message-for"] = id;
            message.AddCssClass(errorClass ?? "field-validation-error");
            message.Attributes["data-error-message"] = errorMessage ?? "Please fill in the reCAPTCHA before submitting";

            return MvcHtmlString.Create(string.Concat(recaptchaDiv.ToString(), message.ToString()));
        }
    }
}