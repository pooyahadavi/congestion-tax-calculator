using CongestionTaxCalculator.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Configuration
{
    public sealed class VehicleEntityConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("Vehicles");
            builder.HasKey(vehicle => vehicle.Id);
            builder.Property(vehicle => vehicle.Name).HasColumnType("varchar(50)").IsRequired();
            
            builder.HasIndex(vehicle => vehicle.Name).IsUnique();

            builder.HasData(
            [
                new("Car") { Id = 1 },
                new("Emergency") { Id = 2 },
                new("Buss") { Id = 3 },
                new("Diplomat") { Id = 4 },
                new("Motorcycle") { Id = 5 },
                new("Military") { Id = 6 },
                new("Foreign") { Id = 7 }
            ]);
        }
    }
}
