using CongestionTaxCalculator.Core.Entities;
using CongestionTaxCalculator.Core.Exceptions;
using CongestionTaxCalculator.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.UnitTest.Entities
{
    public class CityTests
    {
        [Fact]
        public void CreateSuccess()
        {
            var money = new Money(10, "SEK");
            var city = new City("Tehran", money, Enums.DayOfWeek.Sunday, 60);

            Assert.Equal("Tehran", city.Name);
            Assert.Equal(money, city.MaxDailyCharge);
            Assert.Equal(Enums.DayOfWeek.Sunday, city.TollFreeDays);
            Assert.Equal((short)60, city.SingleChargeRuleMinutes);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ThrowsIfNameIsNullOrEmpty(string? name)
        {
            void Action() => new City(name, new Money(10, "SEK"), Enums.DayOfWeek.Sunday, 60);

            Assert.Throws<ApplicationArgumentException>("name", Action);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ThrowsIfSingleChargeRuleMinutesIsZeroOrNegative(short minutes)
        {
            void Action() => new City("Tehran", new Money(10, "SEK"), Enums.DayOfWeek.Sunday, minutes);

            Assert.Throws<ApplicationArgumentException>("singleChargeRuleMinutes", Action);
        }

        [Fact]
        public void AddTaxRuleSuccess()
        {
            var city = new City("Tehran", new Money(10, "SEK"), Enums.DayOfWeek.Sunday, 60);
            var rule = new TaxRule(1, new Money(10, "SEK"), new TimeRange(new TimeOnly(1, 0, 0), new TimeOnly(2, 0, 0)));
            city.AddTaxRule(rule);

            Assert.Single(city.TaxRules);
            Assert.Contains(rule,city.TaxRules);
        }

        [Fact]
        public void ThrowsIfOverlappingTaxRuleIsAdded()
        {
            var city = new City("Tehran", new Money(10, "SEK"), Enums.DayOfWeek.Sunday, 60);
            var rule = new TaxRule(1, new Money(10, "SEK"), new TimeRange(new TimeOnly(1, 0, 0), new TimeOnly(2, 0, 0)));
            city.AddTaxRule(rule);

            void Action() => city.AddTaxRule(new TaxRule(1, new Money(10, "SEK"), new TimeRange(new TimeOnly(1, 30, 0), new TimeOnly(3, 0, 0))));
            Assert.Throws<ApplicationInvalidOperationException>( Action);
        }

        [Fact]
        public void AddTollFreeDateSuccess()
        {
            var city = new City("Tehran", new Money(10, "SEK"), Enums.DayOfWeek.Sunday, 60);
            var tollFreeDate = new TollFreeDate(1, new DateOnly(2013, 1, 1));
            city.AddTollFreeDate(tollFreeDate);

            Assert.Single(city.TollFreeDates);
            Assert.Contains(tollFreeDate, city.TollFreeDates);
        }

        [Fact]
        public void ThrowsIfDuplicateTollFreeDateIsAdded()
        {
            var city = new City("Tehran", new Money(10, "SEK"), Enums.DayOfWeek.Sunday, 60);
            var tollFreeDate = new TollFreeDate(1, new DateOnly(2013, 1, 1));
            city.AddTollFreeDate(tollFreeDate);

            void Action() => city.AddTollFreeDate(tollFreeDate);
            Assert.Throws<ApplicationInvalidOperationException>(Action);
        }

        [Fact]
        public void AddExemptVehicleSuccess()
        {
            var city = new City("Tehran", new Money(10, "SEK"), Enums.DayOfWeek.Sunday, 60);
            var exemptCityVehicle = new ExemptCityVehicle(1, 2);
            city.AddExemptVehicle(exemptCityVehicle);

            Assert.Single(city.ExemptCityVehicles);
            Assert.Contains(exemptCityVehicle, city.ExemptCityVehicles);
        }

        [Fact]
        public void ThrowsIfDuplicateExemptVehicleIsAdded()
        {
            var city = new City("Tehran", new Money(10, "SEK"), Enums.DayOfWeek.Sunday, 60);
            var exemptCityVehicle = new ExemptCityVehicle(1, 2);
            city.AddExemptVehicle(exemptCityVehicle);

            void Action() => city.AddExemptVehicle(exemptCityVehicle);
            Assert.Throws<ApplicationInvalidOperationException>(Action);
        }
    }
}
