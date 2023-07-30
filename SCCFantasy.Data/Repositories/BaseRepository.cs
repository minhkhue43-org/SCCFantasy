using Newtonsoft.Json;
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
        protected readonly string CosmosDBEndPointUri;
        protected readonly string CosmosDBKey;
        protected readonly string DatabaseName;

        public BaseRepository()
        {
            var databaseConfigPath = Environment.CurrentDirectory + "\\databaseConfig.json";

            if (File.Exists(databaseConfigPath))
            {
                var databaseConfigJsonString = File.ReadAllText(databaseConfigPath);
                var databaseConfig = JsonConvert.DeserializeObject<DatabaseConfig>(databaseConfigJsonString);

                this.CosmosDBEndPointUri = databaseConfig.CosmosDBEndPointUri;
                this.CosmosDBKey = databaseConfig.CosmosDBKey;
                this.DatabaseName = databaseConfig.DatabaseName;
            }
            else
            {
                this.CosmosDBEndPointUri = Environment.GetEnvironmentVariable("CosmosDBEndPointUri");
                this.CosmosDBKey = Environment.GetEnvironmentVariable("CosmosDBKey");
                this.DatabaseName = Environment.GetEnvironmentVariable("DatabaseName");
            }
        }

        private class DatabaseConfig
        {
            public string CosmosDBEndPointUri { get; set; }

            public string CosmosDBKey { get; set; }

            public string DatabaseName { get; set; }
        }
    }
}
