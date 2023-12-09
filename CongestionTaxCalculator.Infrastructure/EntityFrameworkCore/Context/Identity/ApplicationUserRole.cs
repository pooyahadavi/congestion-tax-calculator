using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Context.Identity
{
    public sealed class ApplicationUserRole : IdentityUserRole<int>
    {
        private ApplicationUserRole() { }
    }
}
