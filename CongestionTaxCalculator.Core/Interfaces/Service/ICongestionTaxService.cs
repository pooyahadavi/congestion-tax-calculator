using CongestionTaxCalculator.Core.Dto.Service.CongestionTaxService;
using CongestionTaxCalculator.Core.General;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.Interfaces.Service
{
    public interface ICongestionTaxService
    {
        Task<Result<CalculateCongestionTaxResponseDto>> CalculateCongestionTaxAsync([NotNull] CalculateCongestionTaxRequestDto request);
    }
}
