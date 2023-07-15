using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.ApiServices.Models.Dto
{
    public class PlayerDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ClubName { get; set; }

        public int TeamId { get; set; }

        public int TotalPoints { get; set; }

        public int NowCost { get; set; }

        public int PositionId { get; set; }

        public decimal SelectedPercent { get; set; }
    }
}
