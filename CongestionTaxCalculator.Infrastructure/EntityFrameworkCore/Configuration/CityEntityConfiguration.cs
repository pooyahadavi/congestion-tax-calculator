using CongestionTaxCalculator.Core.Entities;
using CongestionTaxCalculator.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DayOfWeek = CongestionTaxCalculator.Core.Enums.DayOfWeek;

namespace CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Configuration
{
    public sealed class CityEntityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities");
            builder.HasKey(city => city.Id);
            builder.Property(city => city.Name).HasColumnType("varchar(50)").IsRequired();
            builder.Property(city => city.TollFreeDays).HasConversion(t => (byte?)t, t => (DayOfWeek?)t).HasColumnType("tinyint");
            builder.Property(city => city.SingleChargeRuleMinutes).HasColumnType("smallint");
            
            builder.HasIndex(city => city.Name).IsUnique();

            builder.OwnsOne(city => city.MaxDailyCharge, b =>
            {
                b.Property(money => money.Amount).HasColumnName("MaxDailyCharge_Amount").HasColumnType("decimal(19,4)");
                b.Property(money => money.Currency).HasColumnName("MaxDailyCharge_Currency").HasColumnType("char(3)");
            }).HasData(new
            {
                Id = 1,
                Name = "Gothenburg",
                Money_Amount = 60m,
                Money_Currency = "SEK",
                SingleChargeRuleMinutes = (short)60,
                TollFreeDays = DayOfWeek.Saturday | DayOfWeek.Sunday
            });
        }
    }
}
