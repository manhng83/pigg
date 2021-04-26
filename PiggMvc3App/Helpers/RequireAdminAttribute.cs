using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PiggMvc3App.Helpers
{    
    public class RequireAdminAttribute : AuthorizeAttribute
    {        
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return System.Web.Security.Roles.IsUserInRole(httpContext.User.Identity.Name, Constants.AdminRole);
        }
    }
}