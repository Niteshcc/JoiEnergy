using JOIEnergy.Extensions;
using JOIEnergy.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JOIEnergy.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJWTTokenManagerService jwtTokenManagerService)
        {
            UpdateUserProfile(context, jwtTokenManagerService);
            await _next(context);
        }

        private static void UpdateUserProfile(HttpContext context, IJWTTokenManagerService jwtTokenManagerService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var userProfile = jwtTokenManagerService.ValidateToken(token);
                if (userProfile != null)
                {
                    // attach user to context on successful jwt validation
                    context.SetUserProfile(userProfile);
                }
            }
        }
    }
}
