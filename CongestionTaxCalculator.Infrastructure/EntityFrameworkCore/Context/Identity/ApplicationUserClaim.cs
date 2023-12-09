using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Context.Identity
{
    public sealed class ApplicationUserClaim : IdentityUserClaim<int>
    {
        private ApplicationUserClaim() { }
    }
}
