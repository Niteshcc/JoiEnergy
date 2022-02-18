using System;
using System.Net;

namespace JOIEnergy.Exceptions
{
    public class AppException: Exception
    {
        public HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

        public AppException(string message): base(message)
        {
        }
    }
}
