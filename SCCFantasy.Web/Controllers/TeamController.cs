using Microsoft.AspNetCore.Mvc;
using SCCFantasy.Web.Attributes;
using SCCFantasy.Web.Services;

namespace SCCFantasy.Web.Controllers
{
    public class TeamController : BaseController
    {
        private readonly ILogger<TeamController> _logger;
        private readonly IPlayerWebService _playerWebService;
        private readonly IUserWebService _userWebService;

        public TeamController(ILogger<TeamController> logger, IPlayerWebService playerWebService, IUserWebService userWebService)
        {
            _logger = logger;
            _playerWebService = playerWebService;
            _userWebService = userWebService;
        }

        [Authentication]
        public async Task<ActionResult> Index()
        {
            ViewData["Players"] = await _playerWebService.GetPlayers();
            var userId = HttpContext.Session.GetString("UserId");

            var userModel = await _userWebService.GetUserModel(userId);
            ViewData["Team"] = userModel.Team;

            return View();
        }

        [HttpPost]
        [Authentication]
        public async Task<ActionResult> UpdateTeam(string team)
        {
            var userId = HttpContext.Session.GetString("UserId");

            var updateResult = await _userWebService.UpdateUserTeam(userId, team);

            if (updateResult.HasErrors)
            {
                return Error(updateResult.Errors);
            }

            return Success();
        }
    }
}
