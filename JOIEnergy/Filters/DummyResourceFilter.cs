using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace JOIEnergy.Filters
{
    public class DummyResourceFilter : Attribute, IResourceFilter
    {
        public void OnException(ExceptionContext context)
        {
            Console.WriteLine("[DummyResourceFilter- OnException] - Exception occured.");
            Console.WriteLine(context.Exception.Message);
            Console.WriteLine(context.HttpContext.Response.StatusCode);
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine("[DummyResourceFilter] - After action execution.");
            Console.WriteLine(context.HttpContext.Response.StatusCode);
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine("[DummyResourceFilter] - Before action execution.");
            Console.WriteLine(context.HttpContext.Response.StatusCode);
        }
    }
}
