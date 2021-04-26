using Pigg.Contracts;
using Pigg.CQRS.ReadModel;
using System.Web.Mvc;

namespace PiggMvc.Controllers
{
    public class PageController : Controller
    {
        private IReadModelFacade _readModelFacade;
        private ILocalizator _localizator;

        public PageController(IReadModelFacade readModelFacade, ILocalizator localizator)
        {
            _readModelFacade = readModelFacade;
            _localizator = localizator;
            _localizator.SetCulture();
        }

        public ActionResult GetPage(int customPageId)
        {
            var page = _readModelFacade.GetPage(customPageId);
            return View("Details", page);
        }
    }
}