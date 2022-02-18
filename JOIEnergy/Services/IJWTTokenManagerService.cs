using JOIEnergy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JOIEnergy.Services
{
    public interface IJWTTokenManagerService
    {
        string GenerateToken(string user, string pwd);
        UserProfile ValidateToken(string jwtToken);
    }
}
