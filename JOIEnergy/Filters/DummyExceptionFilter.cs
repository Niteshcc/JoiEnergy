using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;
using System.Text;

namespace JOIEnergy.Filters
{
    public class DummyExceptionFilter : Attribute,  IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine("[DummyExceptionFilter] - Exception occured.");
            Console.WriteLine(context.Exception.Message);
            Console.WriteLine(context.HttpContext.Response.StatusCode);
            byte[] reqData = new byte[2000];
            context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            context.HttpContext.Request.Body.Read(reqData, 0, 2000);
            var req = Encoding.UTF8.GetString(reqData);
            Console.WriteLine(req);
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("[DummyExceptionFilter] - After action execution.");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("[DummyExceptionFilter] - Before action execution.");
        }
    }
}
