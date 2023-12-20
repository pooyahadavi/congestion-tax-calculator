using CongestionTaxCalculator.Core.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CongestionTaxCalculator.Api.Extensions;
using static CongestionTaxCalculator.Core.General.Constants;
using CongestionTaxCalculator.Api.Controllers;
using CongestionTaxCalculator.Core.Dto.Service.CongestionTaxService;
using CongestionTaxCalculator.Core.Interfaces.Service;
using CongestionTaxCalculator.Core.ViewModel.City;
using Moq;
using CongestionTaxCalculator.Core.ValueObjects;

namespace CongestionTaxCalculator.Api.UnitTest.Controllers
{
	public class CitiesControllerTests
	{
		[Fact]
		public async Task CalculateCongestionTax_ReturnsOkObjectResult()
		{
			var money = new Money(10, "SEK");
			var congestionTaxServiceMock = new Mock<ICongestionTaxService>();
			congestionTaxServiceMock.Setup(service =>
				service.CalculateCongestionTaxAsync(It.IsAny<CalculateCongestionTaxRequestDto>()))
				.ReturnsAsync(new Result<CalculateCongestionTaxResponseDto>(OperationResult.Succeeded)
				{
					Data = new CalculateCongestionTaxResponseDto
					{
						Charge = money
					}
				});

			var controller = new CitiesController(congestionTaxServiceMock.Object);

			var result = await controller.CalculateCongestionTax(1, new CalculateCongestionTaxRequestViewModel
			{
				VehicleId = 1,
				DateTimes = new List<DateTime> { DateTime.Now }
			});

			var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
			var viewModel = Assert.IsType<CalculateCongestionTaxResponseViewModel>(okObjectResult.Value);
			Assert.Equal(money, viewModel.Charge);
		}

		[Fact]
		public async Task CalculateCongestionTax_ReturnsBadRequestResult()
		{
			var congestionTaxServiceMock = new Mock<ICongestionTaxService>();
			congestionTaxServiceMock.Setup(service =>
				service.CalculateCongestionTaxAsync(It.IsAny<CalculateCongestionTaxRequestDto>()))
				.ReturnsAsync(new Result<CalculateCongestionTaxResponseDto>(OperationResult.NotValid) { Error = "Validation Error" });

			var controller = new CitiesController(congestionTaxServiceMock.Object);

			var result = await controller.CalculateCongestionTax(1, new CalculateCongestionTaxRequestViewModel
			{
				VehicleId = 1,
				DateTimes = new List<DateTime> { DateTime.Now }
			});

			var badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result.Result);
			var error = Assert.IsType<Error>(badRequestObjectResult.Value);
			Assert.Equal("Validation Error", error.Message);
		}
	}
}
