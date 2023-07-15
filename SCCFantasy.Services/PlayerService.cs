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
        Task<List<PlayerDto>> GetPlayers();
    }

    public class PlayerService : IPlayerService
    {
        public PlayerService()
        {
                
        }

        public async Task<List<PlayerDto>> GetPlayers()
        {
            var players = new List<PlayerDto>();

            players.Add(new PlayerDto
            {
                Id = 1,
                FirstName = "Minh Khue",
                LastName = "Le",
                Age = 30,
                ClubName = "MU"
            });

            return players;
        }

    }
}
