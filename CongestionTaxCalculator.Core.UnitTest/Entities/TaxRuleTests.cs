using CongestionTaxCalculator.Core.Entities;
using CongestionTaxCalculator.Core.ValueObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.UnitTest.Entities
{
    public class TaxRuleTests
    {
        [Fact]
        public void CreateSuccess()
        {
            var money = new Money(10, "SEK");
            var timeRange = new TimeRange(new TimeOnly(1, 0, 0), new TimeOnly(2, 0, 0));
            var taxRule = new TaxRule(1, money, timeRange);

            Assert.Equal(1, taxRule.CityId);
            Assert.Equal(money, taxRule.Charge);
            Assert.Equal(timeRange, taxRule.TimeRange);
        }
    }
}
