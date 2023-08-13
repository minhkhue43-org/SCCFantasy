using Newtonsoft.Json;
using SCCFantasy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.Data.Repositories
{
    public class BaseRepository
    {
        protected readonly string DatabaseName;

        public BaseRepository()
        {
            this.DatabaseName = GlobalConstants.DatabaseName;
        }
    }
}
