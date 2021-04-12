using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Fupi_WebApplication.DTOs;
using Fupi_WebApplication.Models;
using Fupi_WebApplication.Repositories;
using Fupi_WebApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Fupi_WebApplication.Controllers
{
    public class FupiController : Controller
    {
        private ILogger<FupiController> logger;
        private FupiKeyService fupiKeyService;
        private IWrapper wrapper;
        private IMemoryCache memoryCache;
        public FupiController(FupiKeyService keyService, IWrapper wrapper, IMemoryCache _memoryCache, ILogger<FupiController> logger)
        {
            this.fupiKeyService = keyService;
            this.wrapper = wrapper;
            this.memoryCache = _memoryCache;
            this.logger = logger;
        }


        [Route("")]
        [Route("Fupi")]
        [Route("Fupi/{proxy}")]
        // GET: FupiController
        public async Task<ActionResult> Index(string proxy)
        {
            // Proxy has value fetch the Value and redirect to it.

            try
            {
                if (!String.IsNullOrEmpty(proxy))
                {

                    FupiModel fupiObj = await memoryCache.GetOrCreateAsync(proxy, entry =>
                                    {
                                        FupiModel fupi = this.wrapper.Repo.GetFupiUrl(v => v.ProxyCode == proxy).GetAwaiter().GetResult();
                                        return Task.FromResult(fupi);
                                    });


                    if (fupiObj != null)
                    {
                        // update visit count
                        ++fupiObj.AccessCount;
                        await this.wrapper.Repo.Update(fupiObj);
                        await this.wrapper.SaveAsync();
                        return new RedirectResult(fupiObj.OriginalUrl);
                    }
                    else
                    {
                        ViewBag.Error = "Url path not found";
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                ViewBag.Error = $"{ex.Message}";
                return View();
            }
        }
        [Route("Fupi/AddUrl")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm]FupiRequestDto fupiRequest)
        {
            try
            {
                // get the url
                
                if (ModelState.IsValid)
                {
                    // fetch proxy from key gen and pass request
                    FupiKeyGenDto genDto = await this.fupiKeyService.GetKey();

                    if(genDto != null)
                    {
                        // wrap and create a new database entry
                        await this.wrapper.Repo.AddUrl(new Models.FupiModel
                        {
                            AccessCount = 0,
                            CreatedDate = genDto.CreatedTime,
                            ExpiryDate = DateTime.Now.AddMonths(1),
                            IssuedDate = DateTime.Now,
                            OriginalUrl = fupiRequest.Url,
                            ProxyCode = genDto.Proxy
                        });

                        // save changes
                        await this.wrapper.SaveAsync();

                        ViewBag.FupiUrl = $"https://fupi.url/{genDto.Proxy}";
                    }
                    else
                    {
                        this.logger.LogError("Keys, Unavailable");
                        ViewBag.Error = "No Key Available";
                    }
                }
                //else
                //{
                //    ViewBag.Error = "Enter a Url";
                //}

                return View();
            }
            catch(Exception ex)
            {
                this.logger.LogError(ex, ex.Message);
                ViewBag.Error = $"{ex.Message}";
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
