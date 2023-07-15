using SCCFantasy.ApiServices.Api;
using SCCFantasy.ApiServices.Models.Api;
using SCCFantasy.ApiServices.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.Services
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerDto>> GetPlayers();
    }

    public class PlayerService : IPlayerService
    {
        private readonly IGeneralInformationApi _generalInformationApi;
        private readonly IFixturesApi _fixturesApi;

        public PlayerService(IGeneralInformationApi generalInformationApi, IFixturesApi fixturesApi)
        {
            _generalInformationApi = generalInformationApi;
            _fixturesApi = fixturesApi;
        }

        public async Task<IEnumerable<PlayerDto>> GetPlayers()
        {
            BoostrapStaticApiModel generalInfo = await _generalInformationApi.GetGeneralInformation();

            EventApiModel nextEvent = generalInfo.events.First(x => x.is_next);

            List<FixturesApiModel> nextFixtures = await _fixturesApi.GetFixturesByEventId(nextEvent.id);

            IEnumerable<PlayerInfoApiModel> players = generalInfo.elements;

            return players.Select(x => ToPlayerDto(x, nextFixtures));
        }

        private PlayerDto ToPlayerDto(PlayerInfoApiModel apiModel, List<FixturesApiModel> nextFixtures)
        {
            var teamId = apiModel.team;
            var fixtures = nextFixtures.Where(x => x.team_a == teamId || x.team_h == teamId);

            var nextOpponentTeamId = new List<int>();

            foreach (var fixture in fixtures)
            {
                if (fixture.team_a == teamId)
                {
                    nextOpponentTeamId.Add(fixture.team_h);
                }
                else
                {
                    nextOpponentTeamId.Add(fixture.team_a);
                }
            }

            return new PlayerDto
            { 
                Id = apiModel.id,
                FirstName = apiModel.first_name,
                LastName = apiModel.second_name,
                TeamId = apiModel.team,
                TotalPoints = apiModel.total_points,
                NowCost = apiModel.now_cost,
                PositionId = apiModel.element_type,
                SelectedPercent = decimal.Parse(apiModel.selected_by_percent),
                NextOpponentTeamIds = nextOpponentTeamId.ToArray()
            };
        }
    }
}
