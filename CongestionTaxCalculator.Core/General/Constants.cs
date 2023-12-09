using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.General
{
    public static class Constants
    {
        public const string ConnectionStringKey = "CongestionTaxCalculator_ConnectionString";
        public const string JwtSecretKey = "CongestionTaxCalculator_JwtSecret";
        public const string SuperadminPasswordKey = "CongestionTaxCalculator_SuperadminPassword";

        public enum OperationResult : byte
        {
            NotFound = 0,
            Succeeded = 1,
            Failed = 2,
            NotValid = 3,
        }
    }
}
