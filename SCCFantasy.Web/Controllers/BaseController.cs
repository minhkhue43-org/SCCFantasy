using Microsoft.AspNetCore.Mvc;

namespace SCCFantasy.Web.Controllers
{
    public class BaseController : Controller
    {
        public ActionResult UnderConstruction()
        {
            return View();
        }

        protected virtual JsonResult Success(object data = null)
        {
            return Json(new { success = true, data = data });
        }

        protected virtual JsonResult Error(object data = null)
        {
            return Json(new { success = false, errors = data });
        }
    }
}
