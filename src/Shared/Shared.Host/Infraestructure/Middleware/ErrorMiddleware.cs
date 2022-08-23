namespace Shared.Host.Infraestructure.Middleware
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public class ErrorMiddleware : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Path.StartsWithSegments("/middleware", out _, out var remaining))
            {
                if (remaining.StartsWithSegments("/error"))
                {
                    throw new Exception("This is an exception thrown from middleware.");
                }
            }

            return next(context);
        }
    }
}
