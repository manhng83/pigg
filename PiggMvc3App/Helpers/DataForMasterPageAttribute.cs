using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Configuration;
using Microsoft.Practices.Unity;
using PiggMvc3App.Models;
using Pigg.Contracts.Repositories;

namespace PiggMvc3App.Helpers
{
    public class DataForMasterPageAttribute : ActionFilterAttribute 
    {
        [Dependency]
        public ICustomPageRepository CustomPageRepository { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Optional: Work only for GET request 
            //if (filterContext.RequestContext.HttpContext.Request.RequestType != "GET")
            //    return;

            // Optional: Do not work with AjaxRequests 
            //if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            //    return;

            string lang = ConfigurationManager.AppSettings[Constants.DefaultLanguage];
            if (HttpContext.Current.Request["lang"] != null)
            {
                lang = HttpContext.Current.Request["lang"];
                filterContext.HttpContext.Session[Constants.CurrentLanguage] = lang;
            }
            else
            {
                if (filterContext.HttpContext.Session != null && filterContext.HttpContext.Session[Constants.CurrentLanguage] != null)
                    lang = filterContext.HttpContext.Session[Constants.CurrentLanguage].ToString();
                else
                    filterContext.HttpContext.Session[Constants.CurrentLanguage] = lang;
            }

            filterContext.Controller.ViewBag.MenuItems = CustomPageRepository.GetRootElements(lang);
            if (HttpContext.Current.User.IsInRole(Constants.AdminRole))
            {
                filterContext.Controller.ViewData[Constants.LanguagesExistentKey] = CustomPageRepository.GetPageLanguages();
            }
            else
            {
                filterContext.Controller.ViewData[Constants.LanguagesExistentKey] = CustomPageRepository.GetPublishedPageLanguages();
            }
            
            filterContext.Controller.ViewData[Constants.LanguagesKey] = CultureInfo.GetCultures(CultureTypes.NeutralCultures).ToList();                 
        }
    }
}