using SCCFantasy.Models;
using SCCFantasy.WebCore.Models;
using SCCFantasy.WebCore.Players;

namespace SCCFantasy.Services
{
    public interface IPlayerWebService
    {
        Task<IEnumerable<PlayerModel>> GetPlayers();
    }

    public class PlayerWebService : IPlayerWebService
    {
        private readonly IPlayerService _playerService;

        public PlayerWebService(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public async Task<IEnumerable<PlayerModel>> GetPlayers()
        {
            var players = await _playerService.GetPlayers();

            return players.Select(x => ToPlayerModel(x));
        }

        private PlayerModel ToPlayerModel(Player dto)
        {
            return new PlayerModel 
            { 
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                ClubName = dto.ClubName,
                Age = dto.Age
            };
        }
    }
}
