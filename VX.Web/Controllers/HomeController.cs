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
            /*ViewData["Message"] = service.GetLanguage().Name;*/
            ViewData["Message"] = service.GerWord().Language.Name;

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
