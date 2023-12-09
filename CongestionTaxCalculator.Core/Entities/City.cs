using CongestionTaxCalculator.Core.Enums;
using CongestionTaxCalculator.Core.Exceptions;
using CongestionTaxCalculator.Core.Interfaces.Aggregate;
using CongestionTaxCalculator.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DayOfWeek = CongestionTaxCalculator.Core.Enums.DayOfWeek;

namespace CongestionTaxCalculator.Core.Entities
{
    public sealed class City : IAggregateRoot
    {
        public City(string name, Money? maxDailyCharge = null, DayOfWeek? tollFreeDays = null, short? singleChargeRuleMinutes = null)
        {
            if (singleChargeRuleMinutes.HasValue && singleChargeRuleMinutes <= 0)
            {
                throw new ApplicationArgumentException("Value cannot be negative or zero", nameof(singleChargeRuleMinutes));
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ApplicationArgumentException("Value cannot be empty", nameof(name));
            }

            Name = name;
            MaxDailyCharge = maxDailyCharge;
            TollFreeDays = tollFreeDays;
            SingleChargeRuleMinutes = singleChargeRuleMinutes;
        }

        private City() { }

        public int Id { get; set; }

        public string Name { get; private set; }

        public Money? MaxDailyCharge { get; private set; }

        public DayOfWeek? TollFreeDays { get; private set; }

        public short? SingleChargeRuleMinutes { get; private set; }


        private readonly List<TaxRule> _taxRules = new();
        public IEnumerable<TaxRule> TaxRules => _taxRules.AsReadOnly();


        private readonly List<TollFreeDate> _tollFreeDates = new();
        public IEnumerable<TollFreeDate> TollFreeDates => _tollFreeDates.AsReadOnly();


        private readonly List<ExemptCityVehicle> _exemptCityVehicles = new();
        public IEnumerable<ExemptCityVehicle> ExemptCityVehicles => _exemptCityVehicles.AsReadOnly();

        public void AddTaxRule(TaxRule taxRule)
        {
            if (_taxRules.Exists(rule => rule.TimeRange.Overlaps(taxRule.TimeRange)))
            {
                throw new ApplicationInvalidOperationException("The time range overlaps with the current schedule");
            }
            _taxRules.Add(taxRule);
        }

        public void AddTollFreeDate(TollFreeDate tollFreeDate)
        {
            if (_tollFreeDates.Exists(tfd => tfd.Date == tollFreeDate.Date))
            {
                throw new ApplicationInvalidOperationException("Duplicate toll-free date");
            }
            _tollFreeDates.Add(tollFreeDate);
        }

        public void AddExemptVehicle(ExemptCityVehicle exemptCityVehicle)
        {
            if (_exemptCityVehicles.Exists(ecv => ecv.VehicleId == exemptCityVehicle.VehicleId))
            {
                throw new ApplicationInvalidOperationException("Duplicate exempt vehicle");
            }
            _exemptCityVehicles.Add(exemptCityVehicle);
        }
    }
}
