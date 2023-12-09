using CongestionTaxCalculator.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.UnitTest.Entities
{
    public class VehicleTests
    {
        [Fact]
        public void CreateSuccess()
        {
            var vehicle = new Vehicle("Car");
            Assert.Equal("Car", vehicle.Name);
        }
    }
}
