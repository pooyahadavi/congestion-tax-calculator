using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.ViewModel.City
{
    public sealed class CalculateCongestionTaxRequestViewModel
    {
        [Required]
        public required int? VehicleId { get; set; }

        [Required]
        public required IEnumerable<DateTime>? DateTimes { get; set; }
    }
}
