using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.ApiServices.Models.Api
{
    public class TeamInfoApiModel
    {
        public int id { get; set; }

        public int code { get; set; }

        public int draw { get; set; }

        public int loss { get; set; }

        public int win { get; set; }

        public string name { get; set; }

        public string short_name { get; set; }

        public int points { get; set; }
    }
}
