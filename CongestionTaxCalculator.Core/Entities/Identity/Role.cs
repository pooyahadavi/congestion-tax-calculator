using CongestionTaxCalculator.Core.Interfaces.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.Entities.Identity
{
    public sealed class Role : IAggregateRoot
    {
        public Role(string? name)
        {
            Name = name;
        }
        public Role(int id, string? name)
        {
            Id = id;
            Name = name;
        }
        public int Id { get; private set; }
        public string? Name { get; private set; }
    }
}
