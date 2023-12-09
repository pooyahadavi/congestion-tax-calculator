using CongestionTaxCalculator.Api.Extensions;
using CongestionTaxCalculator.Core.Dto.Service.CongestionTaxService;
using CongestionTaxCalculator.Core.Interfaces.Service;
using CongestionTaxCalculator.Core.ViewModel.City;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace CongestionTaxCalculator.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICongestionTaxService _congestionTaxService;

        public CitiesController(ICongestionTaxService congestionTaxService)
        {
            _congestionTaxService = congestionTaxService;
        }

        [HttpGet("{id:int}/tax")]
        public async Task<ActionResult<CalculateCongestionTaxResponseViewModel>> CalculateCongestionTax([FromRoute] int id, [NotNull, FromQuery] CalculateCongestionTaxRequestViewModel request)
        {
            var result = await _congestionTaxService.CalculateCongestionTaxAsync(new CalculateCongestionTaxRequestDto
            {
                CityId = id,
                VehicleId = request.VehicleId!.Value,
                DateTimes = request.DateTimes!,
            });
            return result.HttpResult(new CalculateCongestionTaxResponseViewModel { Charge = result.Data?.Charge });
        }
    }
}