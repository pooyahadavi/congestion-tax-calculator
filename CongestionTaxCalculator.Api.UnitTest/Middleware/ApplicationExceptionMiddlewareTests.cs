using CongestionTaxCalculator.Api.Middleware;
using CongestionTaxCalculator.Core.Exceptions;
using CongestionTaxCalculator.Core.General;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace CongestionTaxCalculator.Api.UnitTest.Middleware
{
    public class ApplicationExceptionMiddlewareTests
    {
        [Fact]
        public async Task Returns400StatusCodeWithTheRightMessageIfApplicationExceptionIsThrown()
        {
            var middleware = new ApplicationExceptionMiddleware(context => throw new ApplicationArgumentException("Exception Message"));
            var httpContext = new DefaultHttpContext { Response = { Body = new MemoryStream() } };

            await middleware.InvokeAsync(httpContext);

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(httpContext.Response.Body);
            var responseBody = await reader.ReadToEndAsync();
            var error = JsonSerializer.Deserialize<Error>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.Equal(StatusCodes.Status400BadRequest, httpContext.Response.StatusCode);
            Assert.Equal("Exception Message", error?.Message);
        }

        [Fact]
        public async Task Returns500StatusCodeWithTheRightMessageIfNonApplicationExceptionIsThrown()
        {
            var middleware = new ApplicationExceptionMiddleware(context => throw new Exception("Internal Server Error"));
            var httpContext = new DefaultHttpContext { Response = { Body = new MemoryStream() } };

            await middleware.InvokeAsync(httpContext);

            httpContext.Response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(httpContext.Response.Body);
            var responseBody = await reader.ReadToEndAsync();
            var error = JsonSerializer.Deserialize<Error>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Assert.Equal(StatusCodes.Status500InternalServerError, httpContext.Response.StatusCode);
            Assert.Equal("Internal Server Error", error?.Message);
        }
    }
}