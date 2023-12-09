using CongestionTaxCalculator.Core.Entities.Identity;
using CongestionTaxCalculator.Core.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.Interfaces.Repository
{
    public interface IApplicationUserRepository
    {
        Task<User?> FindByUserNameAsync(string userName);
        Task<Result> AddWithPasswordAsync(User entity, string password);
        Task<bool> IsInRoleAsync(User entity, string roleName);
        Task<Result> AddToRoleAsync(User entity, string roleName);
        Task<Result> CreateRoleAsync(Role entity);
        Task<Role?> FindRoleByNameAsync(string roleName);
    }
}
