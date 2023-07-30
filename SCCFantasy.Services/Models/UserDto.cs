using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.Services.Models
{
    public class UserDto : BaseDatabaseDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Roles { get; set; }

        public string Team { get; set; }

        public bool IsActive { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
