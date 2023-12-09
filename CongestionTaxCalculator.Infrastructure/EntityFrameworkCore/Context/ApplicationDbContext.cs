using CongestionTaxCalculator.Core.Entities;
using CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Context.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Configuration;
using CongestionTaxCalculator.Core.General;

namespace CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Context
{
    public sealed class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<ExemptCityVehicle> ExemptedCityVehicles { get; set; }

        public DbSet<TaxRule> TaxRules { get; set; }

        public DbSet<TollFreeDate> TollFreeDates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new CityEntityConfiguration());
            builder.ApplyConfiguration(new VehicleEntityConfiguration());
            builder.ApplyConfiguration(new ExemptCityVehicleEntityConfiguration());
            builder.ApplyConfiguration(new TaxRuleEntityConfiguration());
            builder.ApplyConfiguration(new TollFreeDateEntityConfiguration());
        }
    }
}
