using JOIEnergy.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JOIEnergy.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomMiddlewares(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ExceptionMiddleware>();
            return builder.UseMiddleware<JwtMiddleware>();
        }
    }
}
