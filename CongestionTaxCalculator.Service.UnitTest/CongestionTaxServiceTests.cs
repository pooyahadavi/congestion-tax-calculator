using CongestionTaxCalculator.Core.Dto.Service.CongestionTaxService;
using CongestionTaxCalculator.Core.Entities;
using CongestionTaxCalculator.Core.Interfaces.Repository;
using CongestionTaxCalculator.Core.Specification;
using CongestionTaxCalculator.Core.ValueObjects;
using Moq;
using System.Linq.Expressions;
using static CongestionTaxCalculator.Core.General.Constants;

namespace CongestionTaxCalculator.Service.UnitTest
{
    public class CongestionTaxServiceTests
    {
        private readonly Mock<IRepository<City>> _cityRepositoryMock;

        public CongestionTaxServiceTests()
        {
            _cityRepositoryMock = new Mock<IRepository<City>>();
        }

        [Fact]
        public async Task ReturnsNotFoundIfCityDoesNotExist()
        {
            _cityRepositoryMock.Setup(repository => repository.GetAsync(It.IsAny<SpecificationBase<City>>(), CancellationToken.None))
                .Returns(Task.FromResult<City?>(null));
            var taxService = new CongestionTaxService(_cityRepositoryMock.Object);

            var result = await taxService.CalculateCongestionTaxAsync(new CalculateCongestionTaxRequestDto
            {
                CityId = 1,
                VehicleId = 1,
                DateTimes = new List<DateTime>()
            });

            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.Equal(OperationResult.NotFound, result.OperationResult);
        }

        [Fact]
        public async Task ReturnsNotValidIfCityDoesNotHaveTaxRules()
        {
            var city = new City("Gothenburg", new Money(60m, "SEK"), Core.Enums.DayOfWeek.Saturday | Core.Enums.DayOfWeek.Sunday, 60) { Id = 1 };
            _cityRepositoryMock.Setup(repository => repository.GetAsync(It.IsAny<SpecificationBase<City>>(), CancellationToken.None))
                .Returns(Task.FromResult<City?>(city));

            var taxService = new CongestionTaxService(_cityRepositoryMock.Object);

            var result = await taxService.CalculateCongestionTaxAsync(new CalculateCongestionTaxRequestDto
            {
                CityId = 1,
                VehicleId = 1,
                DateTimes = new List<DateTime>()
            });

            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.Equal(OperationResult.NotValid, result.OperationResult);
        }

        [Fact]
        public async Task ReturnsZeroAmountForExemptVehicle()
        {
            PrepareCity();
            var taxService = new CongestionTaxService(_cityRepositoryMock.Object);

            var result = await taxService.CalculateCongestionTaxAsync(new CalculateCongestionTaxRequestDto
            {
                CityId = 1,
                VehicleId = 2,
                DateTimes = new List<DateTime>
                {
                    new DateTime(2013, 6, 1, 13, 0, 0),
                    new DateTime(2013, 6, 1, 15, 0, 0)
                }
            });

            Assert.Equal(OperationResult.Succeeded, result.OperationResult);
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.Charge);
            Assert.Equal(0, result.Data.Charge.Amount);
            Assert.Equal("SEK", result.Data.Charge.Currency);
        }

        [Fact]
        public async Task ReturnsCorrectAmountForNonTollFreeDaysAndNoSingleChargeRule()
        {
            PrepareCity();
            var taxService = new CongestionTaxService(_cityRepositoryMock.Object);

            var result = await taxService.CalculateCongestionTaxAsync(new CalculateCongestionTaxRequestDto
            {
                CityId = 1,
                VehicleId = 1,
                DateTimes = new List<DateTime>
                {
                    new DateTime(2013, 6, 1, 13, 30, 0),  // 8  SEK
                    new DateTime(2013, 6, 1, 16, 0, 0),   // 18 SEK
                    new DateTime(2013, 6, 2, 19, 0, 0)    // 0  SEK
                }
            });

            Assert.Equal(OperationResult.Succeeded, result.OperationResult);
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.Charge);
            Assert.Equal(26, result.Data.Charge.Amount);
            Assert.Equal("SEK", result.Data.Charge.Currency);
        }

        [Fact]
        public async Task ReturnsCorrectAmountForTollFreeDaysAndNoSingleChargeRule()
        {
            PrepareCity();
            var taxService = new CongestionTaxService(_cityRepositoryMock.Object);

            var result = await taxService.CalculateCongestionTaxAsync(new CalculateCongestionTaxRequestDto
            {
                CityId = 1,
                VehicleId = 1,
                DateTimes = new List<DateTime>
                {
                    new DateTime(2013, 6, 1, 13, 30, 0),  // 8  SEK
                    new DateTime(2013, 6, 1, 16, 0, 0),   // 18 SEK
                    new DateTime(2013, 6, 2, 19, 0, 0),   // 0  SEK
                    new DateTime(2013, 7, 1, 19, 0, 0)    // toll-free
                }
            });

            Assert.Equal(OperationResult.Succeeded, result.OperationResult);
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.Charge);
            Assert.Equal(26, result.Data.Charge.Amount);
            Assert.Equal("SEK", result.Data.Charge.Currency);
        }

        [Fact]
        public async Task ReturnsCorrectAmountForTollFreeDaysAndSingleChargeRule()
        {
            PrepareCity();
            var taxService = new CongestionTaxService(_cityRepositoryMock.Object);

            var result = await taxService.CalculateCongestionTaxAsync(new CalculateCongestionTaxRequestDto
            {
                CityId = 1,
                VehicleId = 1,
                DateTimes = new List<DateTime>
                {
                    new DateTime(2013, 6, 1, 13, 30, 0),   // 8  SEK

                    new DateTime(2013, 6, 1, 15, 15, 0),   // 13 SEK
                    new DateTime(2013, 6, 1, 16, 0, 0),    // 18 SEK   Maximum is 18

                    new DateTime(2013, 6, 2, 19, 0, 0),    // 0  SEK
                    new DateTime(2013, 7, 1, 19, 0, 0),    // toll-free
                    new DateTime(2013, 8, 1, 17, 30, 0)    // 13 SEK
                }
            });

            Assert.Equal(OperationResult.Succeeded, result.OperationResult);
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.NotNull(result.Data.Charge);
            Assert.Equal(39, result.Data.Charge.Amount);
            Assert.Equal("SEK", result.Data.Charge.Currency);
        }

        private void PrepareCity()
        {
            var city = new City("Gothenburg", new Money(60m, "SEK"), Core.Enums.DayOfWeek.Saturday | Core.Enums.DayOfWeek.Sunday, 60) { Id = 1 };

            var exemptVehicles = new List<ExemptCityVehicle>()
            {
                new ExemptCityVehicle(1, 2),
                new ExemptCityVehicle(1, 3),
                new ExemptCityVehicle(1, 4),
                new ExemptCityVehicle(1, 5),
                new ExemptCityVehicle(1, 6),
            };

            var tollFreeDate = new List<TollFreeDate>
            {
                new TollFreeDate(1, new DateOnly(2013, 1, 1)) { Id = 1 },
                new TollFreeDate(1, new DateOnly(2013, 1, 5)) { Id = 2 },
                new TollFreeDate(1, new DateOnly(2013, 1, 6)) { Id = 3 },
                new TollFreeDate(1, new DateOnly(2013, 3, 28)) { Id = 4 },
                new TollFreeDate(1, new DateOnly(2013, 3, 29)) { Id = 5 },
                new TollFreeDate(1, new DateOnly(2013, 3, 30)) { Id = 6 },
                new TollFreeDate(1, new DateOnly(2013, 3, 31)) { Id = 7 },
                new TollFreeDate(1, new DateOnly(2013, 4, 1)) { Id = 8 },
                new TollFreeDate(1, new DateOnly(2013, 4, 30)) { Id = 9 },
                new TollFreeDate(1, new DateOnly(2013, 5, 1)) { Id = 10 },
                new TollFreeDate(1, new DateOnly(2013, 5, 8)) { Id = 11 },
                new TollFreeDate(1, new DateOnly(2013, 5, 9)) { Id = 12 },
                new TollFreeDate(1, new DateOnly(2013, 5, 18)) { Id = 13 },
                new TollFreeDate(1, new DateOnly(2013, 5, 19)) { Id = 14 },
                new TollFreeDate(1, new DateOnly(2013, 5, 20)) { Id = 15 },
                new TollFreeDate(1, new DateOnly(2013, 6, 21)) { Id = 16 },
                new TollFreeDate(1, new DateOnly(2013, 6, 22)) { Id = 17 },
                new TollFreeDate(1, new DateOnly(2013, 7, 1)) { Id = 18 },
                new TollFreeDate(1, new DateOnly(2013, 7, 2)) { Id = 19 },
                new TollFreeDate(1, new DateOnly(2013, 7, 3)) { Id = 20 },
                new TollFreeDate(1, new DateOnly(2013, 7, 4)) { Id = 21 },
                new TollFreeDate(1, new DateOnly(2013, 7, 5)) { Id = 22 },
                new TollFreeDate(1, new DateOnly(2013, 7, 6)) { Id = 23 },
                new TollFreeDate(1, new DateOnly(2013, 7, 7)) { Id = 24 },
                new TollFreeDate(1, new DateOnly(2013, 7, 8)) { Id = 25 },
                new TollFreeDate(1, new DateOnly(2013, 7, 9)) { Id = 26 },
                new TollFreeDate(1, new DateOnly(2013, 7, 10)) { Id = 27 },
                new TollFreeDate(1, new DateOnly(2013, 7, 11)) { Id = 28 },
                new TollFreeDate(1, new DateOnly(2013, 7, 12)) { Id = 29 },
                new TollFreeDate(1, new DateOnly(2013, 7, 13)) { Id = 30 },
                new TollFreeDate(1, new DateOnly(2013, 7, 14)) { Id = 31 },
                new TollFreeDate(1, new DateOnly(2013, 7, 15)) { Id = 32 },
                new TollFreeDate(1, new DateOnly(2013, 7, 16)) { Id = 33 },
                new TollFreeDate(1, new DateOnly(2013, 7, 17)) { Id = 34 },
                new TollFreeDate(1, new DateOnly(2013, 7, 18)) { Id = 35 },
                new TollFreeDate(1, new DateOnly(2013, 7, 19)) { Id = 36 },
                new TollFreeDate(1, new DateOnly(2013, 7, 20)) { Id = 37 },
                new TollFreeDate(1, new DateOnly(2013, 7, 21)) { Id = 38 },
                new TollFreeDate(1, new DateOnly(2013, 7, 22)) { Id = 39 },
                new TollFreeDate(1, new DateOnly(2013, 7, 23)) { Id = 40 },
                new TollFreeDate(1, new DateOnly(2013, 7, 24)) { Id = 41 },
                new TollFreeDate(1, new DateOnly(2013, 7, 25)) { Id = 42 },
                new TollFreeDate(1, new DateOnly(2013, 7, 26)) { Id = 43 },
                new TollFreeDate(1, new DateOnly(2013, 7, 27)) { Id = 44 },
                new TollFreeDate(1, new DateOnly(2013, 7, 28)) { Id = 45 },
                new TollFreeDate(1, new DateOnly(2013, 7, 29)) { Id = 46 },
                new TollFreeDate(1, new DateOnly(2013, 7, 30)) { Id = 47 },
                new TollFreeDate(1, new DateOnly(2013, 7, 31)) { Id = 48 },
                new TollFreeDate(1, new DateOnly(2013, 10, 31)) { Id = 49 },
                new TollFreeDate(1, new DateOnly(2013, 11, 1)) { Id = 50 },
                new TollFreeDate(1, new DateOnly(2013, 12, 24)) { Id = 51 },
                new TollFreeDate(1, new DateOnly(2013, 12, 25)) { Id = 52 },
                new TollFreeDate(1, new DateOnly(2013, 12, 26)) { Id = 53 },
                new TollFreeDate(1, new DateOnly(2013, 12, 30)) { Id = 54 },
                new TollFreeDate(1, new DateOnly(2013, 12, 31)) { Id = 55 },
            };

            var taxRules = new List<TaxRule>
            {
                new TaxRule(1, new Money(0m, "SEK"), new TimeRange(TimeOnly.MinValue, new TimeOnly(6, 0, 0))),
                new TaxRule(1, new Money(8m, "SEK"), new TimeRange(new TimeOnly(6, 0, 0), new TimeOnly(6, 30, 0))),
                new TaxRule(1, new Money(13m, "SEK"), new TimeRange(new TimeOnly(6, 30, 0), new TimeOnly(7, 0, 0))),
                new TaxRule(1, new Money(18m, "SEK"), new TimeRange(new TimeOnly(7, 0, 0), new TimeOnly(8, 0, 0))),
                new TaxRule(1, new Money(13m, "SEK"), new TimeRange(new TimeOnly(8, 0, 0), new TimeOnly(8, 30, 0))),
                new TaxRule(1, new Money(8m, "SEK"), new TimeRange(new TimeOnly(8, 30, 0), new TimeOnly(15, 0, 0))),
                new TaxRule(1, new Money(13m, "SEK"), new TimeRange(new TimeOnly(15, 0, 0), new TimeOnly(15, 30, 0))),
                new TaxRule(1, new Money(18m, "SEK"), new TimeRange(new TimeOnly(15, 30, 0), new TimeOnly(17, 0, 0))),
                new TaxRule(1, new Money(13m, "SEK"), new TimeRange(new TimeOnly(17, 0, 0), new TimeOnly(18, 0, 0))),
                new TaxRule(1, new Money(8m, "SEK"), new TimeRange(new TimeOnly(18, 0, 0), new TimeOnly(18, 30, 0))),
                new TaxRule(1, new Money(0m, "SEK"), new TimeRange(new TimeOnly(18, 30, 0), TimeOnly.MaxValue)),
            };

            exemptVehicles.ForEach(city.AddExemptVehicle);
            tollFreeDate.ForEach(city.AddTollFreeDate);
            taxRules.ForEach(city.AddTaxRule);

            _cityRepositoryMock.Setup(repository => repository.GetAsync(It.IsAny<SpecificationBase<City>>(), CancellationToken.None))
                .Returns(Task.FromResult<City?>(city));
        }
    }
}