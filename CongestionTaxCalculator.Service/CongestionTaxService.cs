using CongestionTaxCalculator.Core.Dto.Service.CongestionTaxService;
using CongestionTaxCalculator.Core.Entities;
using CongestionTaxCalculator.Core.General;
using CongestionTaxCalculator.Core.Interfaces.Repository;
using CongestionTaxCalculator.Core.Interfaces.Service;
using CongestionTaxCalculator.Core.Specification;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using static CongestionTaxCalculator.Core.General.Constants;

namespace CongestionTaxCalculator.Service
{
    public class CongestionTaxService : ICongestionTaxService
    {
        private readonly IRepository<City> _cityRepository;

        public CongestionTaxService(IRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<Result<CalculateCongestionTaxResponseDto>> CalculateCongestionTaxAsync([NotNull] CalculateCongestionTaxRequestDto request)
        {
            var specification = new SpecificationBase<City>(whereExpression: city => city.Id == request.CityId, includeExpression: new List<Expression<Func<City, object>>> { city => city.TaxRules, city => city.TollFreeDates, city => city.ExemptCityVehicles });
            var city = await _cityRepository.GetAsync(specification);
            if (city is null)
            {
                return new Result<CalculateCongestionTaxResponseDto>(OperationResult.NotFound) { Error = "City was not found" };
            }

            if (city.TaxRules.Any() != true)
            {
                return new Result<CalculateCongestionTaxResponseDto>(OperationResult.NotValid) { Error = "No tax rule was found" };
            }

            string currency = city.TaxRules.First().Charge.Currency;

            if (city.ExemptCityVehicles.Any(ecv => ecv.VehicleId == request.VehicleId))
            {
                return new Result<CalculateCongestionTaxResponseDto>(OperationResult.Succeeded) { Data = new() { Charge = new(0, currency) } };
            }
            decimal amount = 0;
            var inputDatetimes = request.DateTimes.ToList();
            inputDatetimes.Sort();

            while (inputDatetimes.Count > 0)
            {
                if (city.TollFreeDates.Select(tfd => tfd.Date).Contains(DateOnly.FromDateTime(inputDatetimes[0].Date)))
                {
                    inputDatetimes.RemoveAt(0);
                    continue;
                }
                var inRange = inputDatetimes.Where(dt => dt <= inputDatetimes[0] + TimeSpan.FromMinutes(city.SingleChargeRuleMinutes ?? 0)).ToList();
                amount += inRange.Select(dt => city.TaxRules.FirstOrDefault(rule => rule.TimeRange.ContainsTime(TimeOnly.FromDateTime(dt)))?.Charge.Amount ?? 0).Max();
                inputDatetimes.RemoveRange(0, inRange.Count);
            }

            return new Result<CalculateCongestionTaxResponseDto>(OperationResult.Succeeded)
            {
                Data = new() { Charge = new(amount, currency) }
            };
        }
    }
}