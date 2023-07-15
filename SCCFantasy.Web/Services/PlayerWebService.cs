﻿using SCCFantasy.ApiServices.Models.Dto;
using SCCFantasy.Common.Enums;
using SCCFantasy.Common.Extensions;
using SCCFantasy.Models;

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

        private PlayerModel ToPlayerModel(PlayerDto dto)
        {
            return new PlayerModel 
            { 
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                ClubName = ((Teams)dto.TeamId).GetDescription(),
                Price = (decimal)(dto.NowCost/10.0),
                PostionName = ((PlayerPositions)dto.PositionId).GetDescription(),
                SelectedPercent = dto.SelectedPercent
            };
        }
    }
}
