using CongestionTaxCalculator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.UnitTest.Entities
{
    public class TollFreeDateTests
    {
        [Fact]
        public void CreateSuccess()
        {
            var date = new DateOnly(2013, 1, 1);
            var tollFreeDate = new TollFreeDate(1, date);
            Assert.Equal(1, tollFreeDate.CityId);
            Assert.Equal(date, tollFreeDate.Date);
        }
    }
}
