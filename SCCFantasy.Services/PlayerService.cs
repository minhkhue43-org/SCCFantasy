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

        public PlayerService(IGeneralInformationApi generalInformationApi)
        {
            _generalInformationApi = generalInformationApi;
        }

        public async Task<IEnumerable<PlayerDto>> GetPlayers()
        {
            IEnumerable<PlayerInfoApiModel> players = (await _generalInformationApi.GetGeneralInformation()).elements;

            return players.Select(x => ToPlayerDto(x));
        }

        private PlayerDto ToPlayerDto(PlayerInfoApiModel apiModel)
        {
            return new PlayerDto 
            { 
                Id = apiModel.id,
                FirstName = apiModel.first_name,
                LastName = apiModel.second_name,
                TeamId = apiModel.team,
                TotalPoints = apiModel.total_points,
                NowCost = apiModel.now_cost,
                PositionId = apiModel.element_type,
                SelectedPercent = decimal.Parse(apiModel.selected_by_percent)
            };
        }
    }
}
