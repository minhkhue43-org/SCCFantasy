using Microsoft.AspNetCore.Mvc;
using SCCFantasy.Controllers;
using SCCFantasy.Services;
using SCCFantasy.Web.Utilities;

namespace SCCFantasy.Web.Controllers
{
    public class TeamController : Controller
    {
        private readonly ILogger<TeamController> _logger;
        private readonly IPlayerWebService _playerWebService;

        public TeamController(ILogger<TeamController> logger, IPlayerWebService playerWebService)
        {
            _logger = logger;
            _playerWebService = playerWebService;
        }

        [Authentication]
        public async Task<ActionResult> Index()
        {
            ViewData["Players"] = await _playerWebService.GetPlayers();
            var userId = HttpContext.Session.GetString("UserId");

            return View();
        }
    }
}
