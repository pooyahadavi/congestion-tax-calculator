using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.Dto.Service.CongestionTaxService
{
    public sealed class CalculateCongestionTaxRequestDto
    {
        public required int CityId { get; set; }

        public required int VehicleId { get; set; }

        public required IEnumerable<DateTime> DateTimes { get; set; }
    }
}
