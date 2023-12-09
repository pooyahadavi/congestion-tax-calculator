using CongestionTaxCalculator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.UnitTest.Entities
{
    public class ExemptCityVehicleTest
    {
        [Fact]
        public void CreateSuccess()
        {
            var exemptCityVehicle = new ExemptCityVehicle(1, 2);
            Assert.Equal(1, exemptCityVehicle.CityId);
            Assert.Equal(2, exemptCityVehicle.VehicleId);
        }
    }
}
