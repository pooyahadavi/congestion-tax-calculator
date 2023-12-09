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
    public sealed class ExemptCityVehicleEntityConfiguration : IEntityTypeConfiguration<ExemptCityVehicle>
    {
        public void Configure(EntityTypeBuilder<ExemptCityVehicle> builder)
        {
            builder.ToTable("ExemptCityVehicles");
            builder.HasKey(ecv => new { ecv.VehicleId, ecv.CityId });
            builder.HasOne<Vehicle>().WithMany(v => v.ExemptCityVehicles).HasForeignKey(e => e.VehicleId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne<City>().WithMany(c => c.ExemptCityVehicles).HasForeignKey(e => e.CityId).OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(ecv => new { ecv.VehicleId, ecv.CityId }).IsUnique();

            builder.HasData(
            [
                new(1, 2),
                new(1, 3),
                new(1, 4),
                new(1, 5),
                new(1, 6),
                new(1, 7)
            ]);
        }
    }
}
