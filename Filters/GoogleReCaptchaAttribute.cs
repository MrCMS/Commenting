using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web.Mvc;
using MrCMS.Web.Apps.Commenting.Settings;
using MrCMS.Website;
using MrCMS.Website.Filters;
using Newtonsoft.Json;

namespace MrCMS.Web.Apps.Commenting.Filters
{
    public class GoogleReCaptchaAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (CurrentRequestData.DatabaseIsInstalled)
            {
                var settings = MrCMSApplication.Get<GoogleReCaptchaSettings>();

                if (settings.Enabled)
                {
                    string googleToken = filterContext.HttpContext.Request["g-recaptcha-response"];
                    if (!string.IsNullOrWhiteSpace(
                        googleToken))
                    {

                        var data = new NameValueCollection
                        {
                            {"response", googleToken},
                            {"secret", settings.Secret}
                        };

                        var googleResponse =
                            new WebClient().UploadValues(new Uri("https://www.google.com/recaptcha/api/siteverify"),
                                "POST", data);
                        var jsonString = Encoding.Default.GetString(googleResponse);
                        var json = JsonConvert.DeserializeObject<GoogleRecaptchaResponse>(jsonString);
                        if (!json.Success)
                            filterContext.Result = new EmptyResult();
                    }
                    else
                    {
                        var contentResult = new ContentResult { Content = "Please Complete Re Captcha" };
                        filterContext.Result = contentResult;
                    }
                }

            }
        }

        public class GoogleRecaptchaResponse
        {
            public bool Success { get; set; }
            public string ErrorCodes { get; set; }
        }
    }
}