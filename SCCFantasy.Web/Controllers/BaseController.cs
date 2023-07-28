using Microsoft.AspNetCore.Mvc;

namespace SCCFantasy.Web.Controllers
{
    public class BaseController : Controller
    {
        public ActionResult UnderConstruction()
        {
            return View();
        }
    }
}
