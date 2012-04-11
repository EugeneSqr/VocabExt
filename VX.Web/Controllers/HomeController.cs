using System.Web.Mvc;
using VX.Web.VocabExtServiceReference;

namespace VX.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var service = new VocabExtServiceClient();
            ViewData["Message"] = service.GetData(1);

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
