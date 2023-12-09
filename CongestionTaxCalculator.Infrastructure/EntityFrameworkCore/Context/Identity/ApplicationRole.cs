using CongestionTaxCalculator.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Context.Identity
{
    public sealed class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole(string? roleName)
        {
            Name = roleName;
        }

        private ApplicationRole() { }
    }
}
