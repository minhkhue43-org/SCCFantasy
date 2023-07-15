using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.ApiServices.Models.Api
{
    public class PlayerInfoApiModel
    {
        public int id { get; set; }

        public string first_name { get; set; }

        public string second_name { get; set; }

        public int team { get; set; }

        public int total_points { get; set; }

        public int now_cost { get; set; }

        public string selected_by_percent { get; set; }

        public int element_type { get; set; }
    }
}
