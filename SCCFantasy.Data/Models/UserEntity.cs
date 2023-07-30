using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.Data.Models
{
    public class UserEntity : BaseEntity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Category => "User";

        public string Team { get; set; }

        public string Roles { get; set; }

        public bool IsActive { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }
    }
}
