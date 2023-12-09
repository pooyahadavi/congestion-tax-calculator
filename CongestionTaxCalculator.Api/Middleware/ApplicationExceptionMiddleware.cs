using CongestionTaxCalculator.Core.General;
using CongestionTaxCalculator.Core.Interfaces.Exception;
using System.Net;

namespace CongestionTaxCalculator.Api.Middleware
{
    public class ApplicationExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ApplicationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";

                if (ex is IApplicationException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(new Error(ex.Message));
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsJsonAsync(new Error("Internal Server Error"));
                }
            }
        }
    }
}
