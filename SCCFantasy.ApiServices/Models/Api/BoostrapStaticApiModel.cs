using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.ApiServices.Models.Api
{
    public class BoostrapStaticApiModel
    {
        public List<EventApiModel> events { get; set; }

        public List<TeamInfoApiModel> teams { get; set; }

        public List<PlayerInfoApiModel> elements { get; set; }
    }
}
