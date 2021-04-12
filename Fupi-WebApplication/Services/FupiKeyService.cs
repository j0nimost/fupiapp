using Fupi_WebApplication.DTOs;
using Fupi_WebApplication.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fupi_WebApplication.Services
{
    public class FupiKeyService
    {
        private IWrapper wrapper;
        private IConfiguration configuration;
        private ILogger<FupiKeyService> _logger;
        private string Url;
        private string Key;
        public FupiKeyService(IWrapper wrapper, IConfiguration configuration, ILogger<FupiKeyService> logger)
        {
            this.wrapper = wrapper;
            this.configuration = configuration;
            this.Url = this.configuration.GetValue<string>("FupiKeyGen:Url");
            this.Key = this.configuration.GetValue<string>("FupiKeyGen:Key");
            this._logger = logger;
        }

        
        public async Task<FupiKeyGenDto> GetKey()
        {
            //Get keys
            try
            {
                using var client = new HttpClient();
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, Url);
                string jsonResponse = "";

                requestMessage.Headers.Add("Accept", "application/json");
                requestMessage.Headers.Add("X-Fupi-API-KEY", this.Key);

                using (var response = await client.SendAsync(requestMessage).ConfigureAwait(false))
                {
                    jsonResponse = await response.Content.ReadAsStringAsync();

                }

                // Deserialize
                return !String.IsNullOrEmpty(jsonResponse) ? JsonConvert.DeserializeObject<FupiKeyGenDto>(jsonResponse): null;

            }
            catch (Exception ex)
            {
                //Log fupikeygen
                this._logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
