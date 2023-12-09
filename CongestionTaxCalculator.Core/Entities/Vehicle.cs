using CongestionTaxCalculator.Core.Interfaces.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.Entities
{
    public sealed class Vehicle : IAggregateRoot
    {
        public Vehicle(string name)
        {
            Name = name;
        }

        private Vehicle() { }

        public int Id { get; set; }

        public string Name { get; private set; }

        private readonly List<ExemptCityVehicle> _exemptCityVehicles = new();
        public IEnumerable<ExemptCityVehicle> ExemptCityVehicles => _exemptCityVehicles.AsReadOnly();
    }
}
