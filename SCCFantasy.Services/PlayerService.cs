using SCCFantasy.ApiServices.Api;
using SCCFantasy.ApiServices.Models.Api;
using SCCFantasy.ApiServices.Models.Dto;
using SCCFantasy.Common.Enums;
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

            var allFixtures = await _fixturesApi.GetAllFixtures();

            var nextEventId = nextEvent.id;

            IEnumerable<FixturesApiModel> nextFixtures = allFixtures.Where(x =>x.Event == nextEventId);

            var nextFiveFixtures = allFixtures.Where(x => x.Event.HasValue && x.Event >= nextEventId && x.Event <= nextEventId + 4);

            IEnumerable<PlayerInfoApiModel> players = generalInfo.elements;

            return players.Select(x => ToPlayerDto(x, nextFixtures, nextFiveFixtures));
        }

        private PlayerDto ToPlayerDto(PlayerInfoApiModel apiModel, IEnumerable<FixturesApiModel> nextFixtures, IEnumerable<FixturesApiModel> nextFiveFixtures)
        {
            var teamId = apiModel.team;
            var fixtures = nextFixtures.Where(x => x.team_a == teamId || x.team_h == teamId);
            var nextFivePlayerfixtures = nextFiveFixtures.Where(x => x.team_a == teamId || x.team_h == teamId);

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
                NextOpponentTeamIds = nextOpponentTeamId.ToArray(),
                NextFiveFixture = GetNextFivePlayerFixtures(teamId, nextFivePlayerfixtures)
            };
        }

        private IEnumerable<PlayerFixtureDto> GetNextFivePlayerFixtures(int playerTeamId, IEnumerable<FixturesApiModel> nextFivePlayerFixtures)
        {
            var playerFixtures = new List<PlayerFixtureDto>();

            foreach (FixturesApiModel fixture in nextFivePlayerFixtures)
            {
                if (fixture.team_a == playerTeamId)
                {
                    playerFixtures.Add(new PlayerFixtureDto 
                    { 
                        Event = fixture.Event.Value,
                        OpponentTeamId = fixture.team_h,
                        Difficult = fixture.team_a_difficulty
                    });
                }
                else
                {
                    playerFixtures.Add(new PlayerFixtureDto
                    {
                        Event = fixture.Event.Value,
                        OpponentTeamId = fixture.team_a,
                        Difficult = fixture.team_h_difficulty
                    });
                }
            }

            return playerFixtures;
        }
    }
}
