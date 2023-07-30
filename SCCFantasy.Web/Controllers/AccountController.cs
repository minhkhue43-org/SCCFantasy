using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using SCCFantasy.Web.Extensions;
using SCCFantasy.Web.Models;
using SCCFantasy.Web.Services;

namespace SCCFantasy.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserWebService _userWebService;

        public AccountController(IUserWebService userWebService)
        {
            _userWebService = userWebService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
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
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Team");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                if (!ModelState.IsValid)
                {
                    return Error(ModelState.ToErrorMessages());
                }

                var registerResult = await _userWebService.Register(model);

                if (registerResult.HasErrors)
                {
                    return Error(registerResult.Errors);
                }

                var user = registerResult.Result;

                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetString("UserId", user.Id);

                return Success();
            }
            else
            {
                return RedirectToAction("Index", "Team");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                if (!ModelState.IsValid)
                {
                    return Error(ModelState.ToErrorMessages());
                }

                var loginResult = await _userWebService.Login(model);

                if (loginResult.HasErrors)
                {
                    return Error(loginResult.Errors);
                }

                var user = loginResult.Result;

                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetString("UserId", user.Id);

                return Success();
            }
            else
            {
                return RedirectToAction("Index", "Team");
            }
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
