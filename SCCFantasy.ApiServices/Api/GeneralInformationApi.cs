﻿using SCCFantasy.ApiServices.Models.Api;
using SCCFantasy.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SCCFantasy.ApiServices.Api
{
    public interface IGeneralInformationApi
    {
        Task<BoostrapStaticApiModel> GetGeneralInformation();
    }

    public class GeneralInformationApi : IGeneralInformationApi
    {
        private readonly string apiUrl = $"{GlobalConstants.ApiPrefix}/bootstrap-static/";

        public async Task<BoostrapStaticApiModel> GetGeneralInformation()
        {
            using HttpClient _client = new HttpClient();

            HttpResponseMessage _response = await _client.GetAsync(apiUrl);

            string _content = await _response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<BoostrapStaticApiModel>(_content);
        }
    }
}
