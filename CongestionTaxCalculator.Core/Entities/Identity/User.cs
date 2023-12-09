using CongestionTaxCalculator.Core.Interfaces.Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.Entities.Identity
{
    public sealed class User : IAggregateRoot
    {
        public User(string? userName)
        {
            UserName = userName;
        }
        public User(int id, string? userName)
        {
            Id = id;
            UserName = userName;
        }
        public int Id { get; private set; }
        public string? UserName { get; private set; }
    }
}
