using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Fupi_KeyGenService.Middlewares
{
    public class FupiAPIKeyMiddleware
    {
        private readonly RequestDelegate _next;

        public FupiAPIKeyMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                string header = "X-Fupi-API-KEY";

                if (!context.Request.Headers.TryGetValue(header, out var extractedApiKey))
                {
                    await HandleError(context, "Missing X-Fupi-API-KEY Header");
                }
                else
                {
                    string key = context.Request.Headers[header];
                    var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();
                    string apiKey = appSettings.GetValue<string>("Fupi:APIKEY");

                    if (!key.Equals(apiKey))
                    {
                        await HandleError(context, "API-KEYs Don't Match");
                    }
                    else
                    {
                        await _next(context);
                    }
                }


            }
            catch (Exception ex)
            {
                await HandleError(context, ex.Message);
            }

        }


        private static Task HandleError(HttpContext context, string ex)
        {

            HttpStatusCode code = HttpStatusCode.InternalServerError; // 500 if unexpected


            string result = JsonConvert.SerializeObject(new { error = ex });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);

        }
    }

    public static class FupiAPIKeyMiddlewareExtension
    {
        public static IApplicationBuilder UseAPIKey(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FupiAPIKeyMiddleware>();
        }
    }
}
