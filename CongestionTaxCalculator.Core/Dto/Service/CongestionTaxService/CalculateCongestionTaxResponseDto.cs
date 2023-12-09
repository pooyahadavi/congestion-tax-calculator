using CongestionTaxCalculator.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.Dto.Service.CongestionTaxService
{
    public sealed class CalculateCongestionTaxResponseDto
    {
        public Money? Charge { get; set; }
    }
}
