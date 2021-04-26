using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PiggMvc3App.Helpers;

namespace PiggMvc3App.Controllers
{
    [DataForMasterPage]
    public class MyBaseController : Controller
    {
        public string CurrentLanguage
        {
            get
            {
                return Session[Constants.CurrentLanguage].ToString();
            }
        }
    }
}
