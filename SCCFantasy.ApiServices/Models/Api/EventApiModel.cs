using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.ApiServices.Models.Api
{
    public class EventApiModel
    {
        public int id { get; set; }

        public string name { get; set; }

        public DateTime deadline_time { get; set; }

        public int average_entry_score { get; set; }

        public bool finished { get; set; }

        public bool data_checked { get; set; }

        public bool is_previous { get; set; }

        public bool is_current { get; set; }

        public bool is_next { get; set; }

        public int transfers_made { get; set; }
    }
}
