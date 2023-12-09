using CongestionTaxCalculator.Core.Entities.Identity;
using CongestionTaxCalculator.Core.General;
using CongestionTaxCalculator.Core.Interfaces.Repository;
using CongestionTaxCalculator.Core.Interfaces.Specification;
using CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Context.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Repository
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ApplicationUserRepository(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<User?> FindByUserNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user is null
                ? null
                : new User(user.Id, user.UserName);
        }

        public async Task<Result> AddWithPasswordAsync(User entity, string password)
        {
            var identityResult = await _userManager.CreateAsync(new ApplicationUser(entity.UserName), password);

            return identityResult.Succeeded
                ? new(Constants.OperationResult.Succeeded)
                : new(Constants.OperationResult.NotValid)
                {
                    Error = string.Join(Environment.NewLine, identityResult.Errors.Select(error => error.Description))
                };
        }

        public async Task<bool> IsInRoleAsync(User entity, string roleName)
        {
            return await _userManager.IsInRoleAsync(new ApplicationUser(entity.Id), roleName);
        }

        public async Task<Result> AddToRoleAsync(User entity, string roleName)
        {
            if (entity.UserName is null)
            {
                return new(Constants.OperationResult.NotFound) { Error = "User was not found" };
            }
            var user = await _userManager.FindByNameAsync(entity.UserName);
            if (user is null)
            {
                return new(Constants.OperationResult.NotFound) { Error = "User was not found"};
            }

            var role = await _roleManager.FindByNameAsync(roleName);
            if (role is null)
            {
                return new(Constants.OperationResult.NotValid) { Error = "Role was not found" };
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded
                ? new(Constants.OperationResult.Succeeded)
                : new(Constants.OperationResult.NotValid)
                {
                    Error = string.Join(Environment.NewLine, result.Errors.Select(error => error.Description))
                };
        }

        public async Task<Result> CreateRoleAsync(Role entity)
        {
            var identityResult = await _roleManager.CreateAsync(new ApplicationRole(entity.Name));

            return identityResult.Succeeded
                ? new(Constants.OperationResult.Succeeded)
                : new(Constants.OperationResult.NotValid)
                {
                    Error = string.Join(Environment.NewLine, identityResult.Errors.Select(error => error.Description))
                };
        }

        public async Task<Role?> FindRoleByNameAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return role is null
                ? null
                : new Role(role.Id, role.Name);
        }
    }
}
