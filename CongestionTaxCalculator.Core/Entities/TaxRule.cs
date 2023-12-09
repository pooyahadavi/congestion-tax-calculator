using CongestionTaxCalculator.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.Entities
{
    public sealed class TaxRule
    {
        public TaxRule(int cityId, Money charge, TimeRange timeRange)
        {
            CityId = cityId;
            Charge = charge;
            TimeRange = timeRange;
        }

        private TaxRule() { }

        public int Id { get; set; }

        public int CityId { get; private set; }

        public Money Charge { get; private set; }

        public TimeRange TimeRange { get; private set; }
    }
}
