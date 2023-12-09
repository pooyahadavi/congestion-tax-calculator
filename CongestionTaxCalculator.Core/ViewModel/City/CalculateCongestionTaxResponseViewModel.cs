using CongestionTaxCalculator.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.ViewModel.City
{
    public sealed class CalculateCongestionTaxResponseViewModel
    {
        public Money? Charge { get; set; }
    }
}
