using SCCFantasy.Services.Models.Dto;
using SCCFantasy.Common.Enums;
using SCCFantasy.Common.Extensions;
using SCCFantasy.Web.Models;
using SCCFantasy.Services;

namespace SCCFantasy.Web.Services
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

        private PlayerModel ToPlayerModel(PlayerDto dto)
        {
            return new PlayerModel 
            { 
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                ClubId = dto.TeamId,
                ClubName = ((Teams)dto.TeamId).GetDescription(),
                Price = (decimal)(dto.NowCost/10.0),
                PositionId = dto.PositionId,
                PositionName = ((PlayerPositions)dto.PositionId).GetDescription(),
                SelectedPercent = dto.SelectedPercent,
                NextOpponents = GetNextOpponentClub(dto.NextOpponentTeamIds),
                NextFivePlayerFixtures = dto.NextFiveFixture.OrderBy(x => x.Event).Select(x => ToPlayerFixtureModel(x))
            };
        }

        private string GetNextOpponentClub(int[] teamIds)
        {
            if (!teamIds.Any())
            {
                return string.Empty;
            }

            return teamIds.Select(x => ((Teams)x).GetDescription()).Aggregate((m,n) => m + ", " + n);
        }

        private PlayerFixtureModel ToPlayerFixtureModel(PlayerFixtureDto dto)
        {
            return new PlayerFixtureModel 
            {
                Event = dto.Event,
                OpponentTeamName = ((Teams)dto.OpponentTeamId).ToString(),
                Difficult = dto.Difficult
            };
        }
    }
}
