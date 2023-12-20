using CongestionTaxCalculator.Core.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CongestionTaxCalculator.Api.Extensions;
using static CongestionTaxCalculator.Core.General.Constants;

namespace CongestionTaxCalculator.Api.UnitTest.Extensions
{
	public class ApplicationExtensionsTests
	{
		[Fact]
		public void HttpResult_Succeeded_ReturnsOkObjectResult()
		{
			var result = new Result(OperationResult.Succeeded);

			var actionResult = result.HttpResult();

			Assert.IsType<OkObjectResult>(actionResult);
		}

		[Fact]
		public void HttpResult_NotFound_ReturnsNotFoundResult()
		{
			var result = new Result(OperationResult.NotFound);

			var actionResult = result.HttpResult();

			Assert.IsType<NotFoundResult>(actionResult);
		}

		[Fact]
		public void HttpResult_NotValid_ReturnsBadRequestObjectResult()
		{
			var result = new Result(OperationResult.NotValid) { Error = "Validation Error" };

			var actionResult = result.HttpResult();

			var badRequestObjectResult = (BadRequestObjectResult)actionResult;
			Assert.IsType<BadRequestObjectResult>(actionResult);
			Assert.IsType<Error>(badRequestObjectResult.Value);
			Assert.Equal("Validation Error", ((Error)badRequestObjectResult.Value).Message);
		}

		[Fact]
		public void HttpResult_Failed_ReturnsStatusCodeResult500()
		{
			var result = new Result(OperationResult.Failed);

			var actionResult = result.HttpResult();

			var statusCodeResult = (StatusCodeResult)actionResult;
			Assert.IsType<StatusCodeResult>(actionResult);
			Assert.Equal(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
		}
	}
}
