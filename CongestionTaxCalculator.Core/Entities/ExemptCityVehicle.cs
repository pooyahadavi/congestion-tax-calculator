using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.Entities
{
    public sealed class ExemptCityVehicle
    {
        public ExemptCityVehicle(int cityId, int vehicleId)
        {
            CityId = cityId;
            VehicleId = vehicleId;
        }

        private ExemptCityVehicle() { }

        public int CityId { get; set; }

        public int VehicleId { get; private set; }
    }
}
