using Fupi_KeyGenService.DTOs;
using Fupi_KeyGenService.Repositories;
using Fupi_KeyGenService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fupi_KeyGenService.Controllers
{
    [Route("api/fupikeygen")]
    [ApiController]
    public class KeyGeneratorController
    {
        private IWrapper wrapper;
        private ILogger<KeyGeneratorController> logger;
        private KeyGeneratorService service;

        public KeyGeneratorController(IWrapper wrapper, ILogger<KeyGeneratorController> logger, KeyGeneratorService service)
        {
            this.logger = logger;
            this.wrapper = wrapper;
            this.service = service;

            service.GetKeyAsync().GetAwaiter().GetResult();
        }

        [HttpGet]
        [Route("getKey")]
        public async Task<ActionResult> getKey()
        {
            try
            {
                //get from cache
                if (KeyGeneratorService.Keys.Count > 10)
                {
                    KeyGenDto key = KeyGeneratorService.Keys.First.Value;
                    KeyGeneratorService.Keys.Remove(key);
                    // Update taken Key
                    this.wrapper.Repository.Update(key);
                    //request new key be created
                    await service.AddKeyAsync();
                    await this.wrapper.SaveAsync();
                    await service.GetKeyAsync();
                    return new OkObjectResult(key);
                }
                else
                {
                    while (KeyGeneratorService.Keys.Count < 10 + 1)
                    {
                        await service.GetKeyAsync();
                        await service.AddKeyAsync();
                    }

                    KeyGenDto key = KeyGeneratorService.Keys.First.Value;
                    KeyGeneratorService.Keys.Remove(key);
                    // Update taken Key
                    this.wrapper.Repository.Update(key);

                    await this.wrapper.SaveAsync();
                    return new OkObjectResult(key);

                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                return new BadRequestObjectResult(ex.Message);
            }


        }
    }
}
