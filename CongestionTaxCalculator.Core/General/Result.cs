using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CongestionTaxCalculator.Core.General.Constants;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CongestionTaxCalculator.Core.General
{
    public class Result
    {
        public Result(OperationResult operationResult)
        {
            OperationResult = operationResult;
        }

        public OperationResult OperationResult { get; set; }

        public string? Error { get; set; }
    }

    public class Result<T> : Result
    {
        public Result(OperationResult operationResult) : base(operationResult)
        {
        }

        public T? Data { get; set; }
    }
}
