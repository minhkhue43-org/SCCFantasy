using SCCFantasy.ApiServices.Models.Api;
using SCCFantasy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SCCFantasy.ApiServices.Api
{
    public interface IFixturesApi
    {
        Task<List<FixturesApiModel>> GetFixturesByEventId(int eventId);
        Task<List<FixturesApiModel>> GetAllFixtures();
    }

    public class FixturesApi : IFixturesApi
    {
        private readonly string allFixturesApiUrl = $"{GlobalConstants.ApiPrefix}/fixtures/";
        private readonly string selectedFixturesApiUrl = $"{GlobalConstants.ApiPrefix}/fixtures/?event=";

        public async Task<List<FixturesApiModel>> GetFixturesByEventId(int eventId)
        {
            using HttpClient _client = new HttpClient();

            HttpResponseMessage _response = await _client.GetAsync($"{selectedFixturesApiUrl}{eventId}");

            string _content = await _response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<FixturesApiModel>>(_content);
        }

        public async Task<List<FixturesApiModel>> GetAllFixtures()
        {
            using HttpClient _client = new HttpClient();

            HttpResponseMessage _response = await _client.GetAsync($"{allFixturesApiUrl}");

            string _content = await _response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<List<FixturesApiModel>>(_content, options);
        }
    }
}
