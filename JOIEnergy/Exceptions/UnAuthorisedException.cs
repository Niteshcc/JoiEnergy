using System;
using System.Net;

namespace JOIEnergy.Exceptions
{
    public class UnAuthorisedException : AppException
    {
        public UnAuthorisedException(string message): base(message)
        {
            this.statusCode = HttpStatusCode.Unauthorized;
        }
    }
}
