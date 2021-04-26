using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace PiggMvc3App.Helpers
{
    public static class LoginExtensions
    {
        public static void LoginStatus(this HtmlHelper htmlHelp)
        {            
            if (HttpContext.Current.User.Identity.IsAuthenticated)
                htmlHelp.RenderPartial("LoginStatusAuthenticatedUserControl");
            else            
                htmlHelp.RenderPartial("LoginStatusAnonymousUserControl");            
        }
    }
}