using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.Services.Models.Dto
{
    public class PlayerFixtureDto
    {
        public int Event { get; set; }

        public int OpponentTeamId { get; set; }

        public int Difficult { get; set; }
    }
}
