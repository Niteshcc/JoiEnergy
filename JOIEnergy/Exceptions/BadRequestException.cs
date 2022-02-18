using System;
using System.Net;

namespace JOIEnergy.Exceptions
{
    public class BadRequestException : AppException
    {
        public BadRequestException(string message): base(message)
        {
            this.statusCode = HttpStatusCode.BadRequest;
        }
    }
}
