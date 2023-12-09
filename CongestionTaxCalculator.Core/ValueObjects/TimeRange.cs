using CongestionTaxCalculator.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.ValueObjects
{
    public record TimeRange
    {
        public TimeRange(TimeOnly start, TimeOnly end)
        {
            if (end <= start)
            {
                throw new ApplicationArgumentException("Invalid time range");
            }

            Start = start;
            End = end;
        }

        public TimeOnly Start { get; set; }

        public TimeOnly End { get; set; }

        public bool Overlaps(TimeRange timeRange)
        {
            return !(timeRange.Start >= End || timeRange.End <= Start);
        }

        public bool ContainsTime(TimeOnly time)
        {
            return time >= Start && time < End;
        }
    }
}
