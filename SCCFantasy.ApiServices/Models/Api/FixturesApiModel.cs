using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.ApiServices.Models.Api
{
    public class FixturesApiModel
    {
        public int id { get;set; }

        public int? Event { get; set; }

        public int team_a { get; set; }

        public int team_h { get; set;}

        public int team_a_difficulty { get; set; }

        public int team_h_difficulty { get; set; }
    }
}
