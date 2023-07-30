using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCFantasy.Common.Utilities
{
    public static class JsonHelper
    {
        public static string SerializeDefault(object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public static T Deserialize<T>(string data, JsonSerializerSettings serializerSettings = null)
        {
            if (data == null)
            {
                return default(T);
            }

            if (serializerSettings != null)
            {
                return JsonConvert.DeserializeObject<T>(data, serializerSettings);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
        }
    }
}
