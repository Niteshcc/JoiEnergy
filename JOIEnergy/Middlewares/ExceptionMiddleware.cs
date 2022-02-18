using JOIEnergy.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Threading.Tasks;

namespace JOIEnergy.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context).ConfigureAwait(false);
            }
            catch (AppException ex)
            {
                context.Response.StatusCode = (int) ex.statusCode;
                context.Response.ContentType = "application/json";
                var response = JsonConvert.SerializeObject(new {error = ex.Message });
                await context.Response.WriteAsync(response).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var response = JsonConvert.SerializeObject(new { error = ex.Message });
                await context.Response.WriteAsync(response).ConfigureAwait(false);
            }
        }
    }
}
