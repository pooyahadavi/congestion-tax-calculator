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
    public sealed class TollFreeDateEntityConfiguration : IEntityTypeConfiguration<TollFreeDate>
    {
        public void Configure(EntityTypeBuilder<TollFreeDate> builder)
        {
            builder.ToTable("TollFreeDates");
            builder.HasKey(tfd => tfd.Id);
            builder.HasOne<City>().WithMany(tfd => tfd.TollFreeDates).HasForeignKey(tfd => tfd.CityId).OnDelete(DeleteBehavior.NoAction);
            builder.Property(tfd => tfd.Date).HasColumnType("date").IsRequired();

            var seed = new List<TollFreeDate>
            {
                new TollFreeDate(1, new DateOnly(2013, 1, 1)) { Id = 1 },
                new TollFreeDate(1, new DateOnly(2013, 1, 5)) { Id = 2 },
                new TollFreeDate(1, new DateOnly(2013, 1, 6)) { Id = 3 },
                new TollFreeDate(1, new DateOnly(2013, 3, 28)) { Id = 4 },
                new TollFreeDate(1, new DateOnly(2013, 3, 29)) { Id = 5 },
                new TollFreeDate(1, new DateOnly(2013, 3, 30)) { Id = 6 },
                new TollFreeDate(1, new DateOnly(2013, 3, 31)) { Id = 7 },
                new TollFreeDate(1, new DateOnly(2013, 4, 1)) { Id = 8 },
                new TollFreeDate(1, new DateOnly(2013, 4, 30)) { Id = 9 },
                new TollFreeDate(1, new DateOnly(2013, 5, 1)) { Id = 10 },
                new TollFreeDate(1, new DateOnly(2013, 5, 8)) { Id = 11 },
                new TollFreeDate(1, new DateOnly(2013, 5, 9)) { Id = 12 },
                new TollFreeDate(1, new DateOnly(2013, 5, 18)) { Id = 13 },
                new TollFreeDate(1, new DateOnly(2013, 5, 19)) { Id = 14 },
                new TollFreeDate(1, new DateOnly(2013, 5, 20)) { Id = 15 },
                new TollFreeDate(1, new DateOnly(2013, 6, 21)) { Id = 16 },
                new TollFreeDate(1, new DateOnly(2013, 6, 22)) { Id = 17 },
                new TollFreeDate(1, new DateOnly(2013, 7, 1)) { Id = 18 },
                new TollFreeDate(1, new DateOnly(2013, 7, 2)) { Id = 19 },
                new TollFreeDate(1, new DateOnly(2013, 7, 3)) { Id = 20 },
                new TollFreeDate(1, new DateOnly(2013, 7, 4)) { Id = 21 },
                new TollFreeDate(1, new DateOnly(2013, 7, 5)) { Id = 22 },
                new TollFreeDate(1, new DateOnly(2013, 7, 6)) { Id = 23 },
                new TollFreeDate(1, new DateOnly(2013, 7, 7)) { Id = 24 },
                new TollFreeDate(1, new DateOnly(2013, 7, 8)) { Id = 25 },
                new TollFreeDate(1, new DateOnly(2013, 7, 9)) { Id = 26 },
                new TollFreeDate(1, new DateOnly(2013, 7, 10)) { Id = 27 },
                new TollFreeDate(1, new DateOnly(2013, 7, 11)) { Id = 28 },
                new TollFreeDate(1, new DateOnly(2013, 7, 12)) { Id = 29 },
                new TollFreeDate(1, new DateOnly(2013, 7, 13)) { Id = 30 },
                new TollFreeDate(1, new DateOnly(2013, 7, 14)) { Id = 31 },
                new TollFreeDate(1, new DateOnly(2013, 7, 15)) { Id = 32 },
                new TollFreeDate(1, new DateOnly(2013, 7, 16)) { Id = 33 },
                new TollFreeDate(1, new DateOnly(2013, 7, 17)) { Id = 34 },
                new TollFreeDate(1, new DateOnly(2013, 7, 18)) { Id = 35 },
                new TollFreeDate(1, new DateOnly(2013, 7, 19)) { Id = 36 },
                new TollFreeDate(1, new DateOnly(2013, 7, 20)) { Id = 37 },
                new TollFreeDate(1, new DateOnly(2013, 7, 21)) { Id = 38 },
                new TollFreeDate(1, new DateOnly(2013, 7, 22)) { Id = 39 },
                new TollFreeDate(1, new DateOnly(2013, 7, 23)) { Id = 40 },
                new TollFreeDate(1, new DateOnly(2013, 7, 24)) { Id = 41 },
                new TollFreeDate(1, new DateOnly(2013, 7, 25)) { Id = 42 },
                new TollFreeDate(1, new DateOnly(2013, 7, 26)) { Id = 43 },
                new TollFreeDate(1, new DateOnly(2013, 7, 27)) { Id = 44 },
                new TollFreeDate(1, new DateOnly(2013, 7, 28)) { Id = 45 },
                new TollFreeDate(1, new DateOnly(2013, 7, 29)) { Id = 46 },
                new TollFreeDate(1, new DateOnly(2013, 7, 30)) { Id = 47 },
                new TollFreeDate(1, new DateOnly(2013, 7, 31)) { Id = 48 },
                new TollFreeDate(1, new DateOnly(2013, 10, 31)) { Id = 49 },
                new TollFreeDate(1, new DateOnly(2013, 11, 1)) { Id = 50 },
                new TollFreeDate(1, new DateOnly(2013, 12, 24)) { Id = 51 },
                new TollFreeDate(1, new DateOnly(2013, 12, 25)) { Id = 52 },
                new TollFreeDate(1, new DateOnly(2013, 12, 26)) { Id = 53 },
                new TollFreeDate(1, new DateOnly(2013, 12, 30)) { Id = 54 },
                new TollFreeDate(1, new DateOnly(2013, 12, 31)) { Id = 55 },
            };

            builder.HasData(seed);
        }
    }
}
