using Pigg.Contracts;
using System.Threading;
using System.Web;

namespace PiggMvc.Models
{
    public class WebLocalizator : ILocalizator
    {
        public WebLocalizator()
        {
            Execute();
        }

        public void SetCulture()
        {
            // Modify current thread's cultures
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(CountryCode);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        private void Execute()
        {
            if (CountryCode == null)
            {
                // Attempt to read the culture cookie from Request
                HttpCookie cultureCookie = HttpContext.Current.Request.Cookies["_culture"];
                if (cultureCookie != null)
                    CountryCode = cultureCookie.Value;
                else if (HttpContext.Current.Request.UserLanguages != null)
                    CountryCode = HttpContext.Current.Request.UserLanguages[0]; // obtain it from HTTP header AcceptLanguages

                // Validate culture name
                //if (!_cultureReader.Exist(CountryCodeFromExternalSource))
                //    CountryCodeFromExternalSource = CultureReader.DEFAULT_COUNTRY_CODE; // This is safe
            }
        }

        public const string CountryCodeFromExternalSource_Key = "CountryCode";

        public string CountryCode
        {
            get
            {
                if (HttpContext.Current.Session[CountryCodeFromExternalSource_Key] == null)
                    return null;
                return HttpContext.Current.Session[CountryCodeFromExternalSource_Key].ToString();
            }
            set
            {
                HttpContext.Current.Session[CountryCodeFromExternalSource_Key] = value;
            }
        }
    }
}