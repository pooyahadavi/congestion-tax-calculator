using CongestionTaxCalculator.Core.Entities;
using CongestionTaxCalculator.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Infrastructure.EntityFrameworkCore.Configuration
{
    public sealed class TaxRuleEntityConfiguration : IEntityTypeConfiguration<TaxRule>
    {
        public void Configure(EntityTypeBuilder<TaxRule> builder)
        {
            builder.ToTable("TaxRules");
            builder.HasKey(rule => rule.Id);
            builder.HasOne<City>().WithMany(city => city.TaxRules).HasForeignKey(rule => rule.CityId).OnDelete(DeleteBehavior.NoAction);
            builder.OwnsOne(rule => rule.Charge, b =>
            {
                b.Property(money => money.Amount).HasColumnName("Charge_Amount").HasColumnType("decimal(19,4)").IsRequired();
                b.Property(money => money.Currency).HasColumnName("Charge_Currency").HasColumnType("char(3)").IsRequired();
            });
            builder.OwnsOne(rule => rule.TimeRange, b =>
            {
                b.Property(timeRange => timeRange.Start).HasColumnName("TimeRange_Start").HasColumnType("time(0)").IsRequired();
                b.Property(timeRange => timeRange.End).HasColumnName("TimeRange_End").HasColumnType("time(0)").IsRequired();
            }).HasData
            ([
                new
                {
                    Id = 1,
                    CityId = 1,
                    Charge_Amount = 0m,
                    Charge_Currency = "SEK",
                    TimeRange_Start = TimeOnly.MinValue,
                    TimeRange_End = new TimeOnly(6, 0, 0),
                },
                new
                {
                    Id = 2,
                    CityId = 1,
                    Charge_Amount = 8m,
                    Charge_Currency = "SEK",
                    TimeRange_Start = new TimeOnly(6, 0, 0),
                    TimeRange_End = new TimeOnly(6, 30, 0),
                },
                new
                {
                    Id = 3,
                    CityId = 1,
                    Charge_Amount = 13m,
                    Charge_Currency = "SEK",
                    TimeRange_Start = new TimeOnly(6, 30, 0),
                    TimeRange_End = new TimeOnly(7, 0, 0),
                },
                new
                {
                    Id = 4,
                    CityId = 1,
                    Charge_Amount = 18m,
                    Charge_Currency = "SEK",
                    TimeRange_Start = new TimeOnly(7, 0, 0),
                    TimeRange_End = new TimeOnly(8, 0, 0),
                },
                new
                {
                    Id = 5,
                    CityId = 1,
                    Charge_Amount = 13m,
                    Charge_Currency = "SEK",
                    TimeRange_Start = new TimeOnly(8, 0, 0),
                    TimeRange_End = new TimeOnly(8, 30, 0),
                },
                new
                {
                    Id = 6,
                    CityId = 1,
                    Charge_Amount = 8m,
                    Charge_Currency = "SEK",
                    TimeRange_Start = new TimeOnly(8, 30, 0),
                    TimeRange_End = new TimeOnly(15, 0, 0),
                },
                new
                {
                    Id = 7,
                    CityId = 1,
                    Charge_Amount = 13m,
                    Charge_Currency = "SEK",
                    TimeRange_Start = new TimeOnly(15, 0, 0),
                    TimeRange_End = new TimeOnly(15, 30, 0),
                },
                new
                {
                    Id = 8,
                    CityId = 1,
                    Charge_Amount = 18m,
                    Charge_Currency = "SEK",
                    TimeRange_Start = new TimeOnly(15, 30, 0),
                    TimeRange_End = new TimeOnly(17, 0, 0),
                },
                new
                {
                    Id = 9,
                    CityId = 1,
                    Charge_Amount = 13m,
                    Charge_Currency = "SEK",
                    TimeRange_Start = new TimeOnly(17, 0, 0),
                    TimeRange_End = new TimeOnly(18, 0, 0),
                },
                new
                {
                    Id = 10,
                    CityId = 1,
                    Charge_Amount = 8m,
                    Charge_Currency = "SEK",
                    TimeRange_Start = new TimeOnly(18, 0, 0),
                    TimeRange_End = new TimeOnly(18, 30, 0),
                },
                new
                {
                    Id = 11,
                    CityId = 1,
                    Charge_Amount = 0m,
                    Charge_Currency = "SEK",
                    TimeRange_Start = new TimeOnly(18, 30, 0),
                    TimeRange_End = TimeOnly.MaxValue,
                }
            ]);
        }
    }
}
