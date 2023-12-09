using CongestionTaxCalculator.Core.Entities;
using CongestionTaxCalculator.Core.Exceptions;
using CongestionTaxCalculator.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.UnitTest.ValueObjects
{
    public class MoneyTests
    {
        [Fact]
        public void CreateSuccess()
        {
            var money = new Money(10, "SEK");
            Assert.Equal(10, money.Amount);
            Assert.Equal("SEK", money.Currency);
        }
        
        [Theory]
        [InlineData("QWERTY")]
        [InlineData("")]
        public void ThrowsIfCurrencyIsInvalid(string currencyName)
        {
            void Action() => new Money(10, currencyName);

            Assert.Throws<ApplicationArgumentException>("currency", Action);
        }

        [Fact]
        public void ThrowsIfAmountIsNegative()
        {
            void Action() => new Money(-10, "SEK");

            Assert.Throws<ApplicationArgumentException>("amount", Action);
        }
    }
}
