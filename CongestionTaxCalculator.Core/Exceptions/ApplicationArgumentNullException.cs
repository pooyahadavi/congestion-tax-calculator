using CongestionTaxCalculator.Core.Interfaces.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.Exceptions
{
    public class ApplicationArgumentNullException : ArgumentNullException, IApplicationException
    {
        public ApplicationArgumentNullException(string message, string paramName) : base(message, paramName)
        {
        }
    }
}
