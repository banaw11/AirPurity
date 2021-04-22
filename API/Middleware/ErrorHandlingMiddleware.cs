using System;
using System.Threading.Tasks;
using API.Middleware.Exceptions;
using Microsoft.AspNetCore.Http;

namespace API.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
              await next.Invoke(context);
            }
            catch(NotFoundException e)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(e.Message);
            }
            catch(Exception e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync($"Something went wrong at {context.Request.Path} \n Error message : [{e.Message}]");
            }
        }
    }
}