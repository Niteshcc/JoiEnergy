using JOIEnergy.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JOIEnergy.Extensions
{
    public static class HttpContextExtentions
    {
        public static void SetUserProfile(this HttpContext context, UserProfile profile)
        {
            context.Items["UserProfile"] = profile;
        }

        public static UserProfile GetUserProfile(this HttpContext context)
        {
            return context.Items["UserProfile"] as UserProfile;
        }
    }
}
