using CongestionTaxCalculator.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.ValueObjects
{
    public sealed record Money
    {
        public Money(decimal amount, string currency)
        {
            if (amount < 0)
            {
                throw new ApplicationArgumentException("Amount cannot be zero or negative", nameof(amount));
            }

            if (string.IsNullOrEmpty(currency) || currency.Length != 3)
            {
                throw new ApplicationArgumentException("Currency is invalid", nameof(currency));
            }

            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; set; }

        public string Currency { get; set; }
    }
}
