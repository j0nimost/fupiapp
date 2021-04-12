using Fupi_KeyGenService.DTOs;
using Fupi_KeyGenService.Models;
using Fupi_KeyGenService.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fupi_KeyGenService.Services
{
    public class KeyGeneratorService
    {
        public static LinkedList<KeyGenDto> Keys = null;
        private ILogger<KeyGeneratorService> logger;
        private IWrapper wrapper;

        public KeyGeneratorService(ILogger<KeyGeneratorService> logger, IWrapper wrapper)
        {
            this.logger = logger;
            this.wrapper = wrapper;
        }

        public async Task AddKeyAsync()
        {
            KeyGenModel keyVal = null;
            //Add what is needed
            bool HasNewKey = false;

            while (HasNewKey == false)
            {
                Guid gkey = Guid.NewGuid();
                string key = gkey.ToString("N");

                key = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(key.Substring(0, 9).ToLower());
                if (!await wrapper.Repository.IsKeyExisting(v => v.Proxy == key))
                {
                    keyVal = new KeyGenModel() { Proxy = key, CreatedTime = DateTime.Now };
                    HasNewKey = true;
                }

            }

            // Add All Keys
            await wrapper.Repository.AddKey(keyVal);
            await wrapper.SaveAsync();

        }

        public async Task GetKeyAsync()
        {
            Keys = null;
            Keys = await this.wrapper.Repository.GetKeys(v => v.IsUtilized == false);

        }
    }
}
