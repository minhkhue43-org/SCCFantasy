using Microsoft.AspNetCore.Mvc;
using SCCFantasy.Web.Models;
using SCCFantasy.Services;
using SCCFantasy.Web.Controllers;
using System.Diagnostics;

namespace SCCFantasy.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPlayerWebService _playerWebService;

        public HomeController(ILogger<HomeController> logger, IPlayerWebService playerWebService)
        {
            _logger = logger;
            _playerWebService = playerWebService;
        }

        public async Task<ActionResult> Index()
        {
            ViewData["Players"] = await _playerWebService.GetPlayers();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}