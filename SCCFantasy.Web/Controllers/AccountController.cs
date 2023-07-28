using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SCCFantasy.Web.Models;
using System.Security.Claims;

namespace SCCFantasy.Web.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet]
        public ActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View("Login");
            }
            else
            {
                return RedirectToAction("Index", "Team");
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                return RedirectToAction("UnderConstruction");
            }
            else
            {
                return RedirectToAction("Index", "Team");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                if (ModelState.IsValid)
                {
                    HttpContext.Session.SetString("UserName", model.UserName);
                    HttpContext.Session.SetString("UserId", "e366adfd-0f1a-45c0-89f9-91c070a495a1");

                    return RedirectToAction("Index", "Team");
                }
            }
            else
            {
                return RedirectToAction("Index", "Team");
            }

            return View();
        }



        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("UserId");

            return RedirectToAction("Login", "Account");
        }
    }
}
