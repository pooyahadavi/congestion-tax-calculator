using CongestionTaxCalculator.Core.Entities.Identity;
using CongestionTaxCalculator.Core.Interfaces.Aggregate;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Context.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser(string? userName)
        {
            UserName = userName;
        }
        public ApplicationUser(int id)
        {
            Id = id;
        }

        private ApplicationUser() { }
    }
}
