using CongestionTaxCalculator.Core.Interfaces.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.Exceptions
{
    public class ApplicationArgumentException : ArgumentException, IApplicationException
    {
        public ApplicationArgumentException(string message) : base(message)
        {
        }
        public ApplicationArgumentException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
