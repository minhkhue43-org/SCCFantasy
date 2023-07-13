using SCCFantasy.WebCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.WebCore.Players
{
    public interface IPlayerService
    {
        Task<List<Player>> GetPlayers();
    }

    public class PlayerService : IPlayerService
    {
        public PlayerService()
        {
                
        }

        public async Task<List<Player>> GetPlayers()
        {
            var players = new List<Player>();

            players.Add(new Player
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
