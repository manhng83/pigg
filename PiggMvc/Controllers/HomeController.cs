using Pigg.Contracts;
using System.Web.Mvc;

namespace PiggMvc.Controllers
{
    public class HomeController : Controller
    {
        private ILocalizator _localizator;
        private IPiggUow _uow;

        public HomeController(ILocalizator localizator, IPiggUow uow)
        {
            _localizator = localizator;
            _uow = uow;
        }

        public ActionResult Index()
        {
            var page = _uow.CustomPageRepository.GetFrontPage(_localizator.CountryCode);
            ViewBag.CountryCode = _localizator.CountryCode;
            return View(page);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}