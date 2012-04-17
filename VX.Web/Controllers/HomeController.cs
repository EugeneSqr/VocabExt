using System.Web.Mvc;
using VX.ServiceFacade;

namespace VX.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var service = new VocabServiceFacade();
            /*ViewData["Message"] = service.GetData(1);*/
            ViewData["Message"] = service.GetLanguage(1).Name;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
