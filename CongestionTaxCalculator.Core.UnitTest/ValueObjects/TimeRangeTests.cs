using CongestionTaxCalculator.Core.Exceptions;
using CongestionTaxCalculator.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.UnitTest.ValueObjects
{
    public class TimeRangeTests
    {
        [Fact]
        public void CreateSuccess()
        {
            var start = new TimeOnly(1, 1, 1);
            var end = new TimeOnly(2, 2, 2);
            var timeRange = new TimeRange(start, end);
            Assert.Equal(start, timeRange.Start);
            Assert.Equal(end, timeRange.End);
        }

        [Fact]
        public void ThrowsIfStartTimeEqualsEndTime()
        {
            var start = new TimeOnly(1, 1, 1);
            var end = new TimeOnly(1, 1, 1);

            void Action() => new TimeRange(start, end);

            Assert.Throws<ApplicationArgumentException>(Action);
        }

        [Fact]
        public void ThrowsIfStartTimeIsBiggerThanEndTime()
        {
            var start = new TimeOnly(2, 1, 1);
            var end = new TimeOnly(1, 1, 1);

            void Action() => new TimeRange(start, end);

            Assert.Throws<ApplicationArgumentException>(Action);
        }

        [Fact]
        public void ReturnsTrueIfTimeRangeOverlaps()
        {
            var timeRange = new TimeRange(new TimeOnly(1, 0, 0), new TimeOnly(3, 0, 0));
            var overlappingTimeRange1 = new TimeRange(new TimeOnly(2, 0, 0), new TimeOnly(4, 0, 0));
            var overlappingTimeRange2 = new TimeRange(new TimeOnly(1, 0, 0), new TimeOnly(4, 0, 0));
            var overlappingTimeRange3 = new TimeRange(new TimeOnly(0, 30, 0), new TimeOnly(1, 30, 0));

            var result1 = timeRange.Overlaps(overlappingTimeRange1);
            var result2 = timeRange.Overlaps(overlappingTimeRange2);
            var result3 = timeRange.Overlaps(overlappingTimeRange3);

            Assert.True(result1);
            Assert.True(result2);
            Assert.True(result3);
        }


        [Fact]
        public void ReturnsFalseIfTimeRangeDoesNotOverlap()
        {
            var timeRange = new TimeRange(new TimeOnly(1, 0, 0), new TimeOnly(3, 0, 0));
            var overlappingTimeRange1 = new TimeRange(new TimeOnly(3, 0, 0), new TimeOnly(4, 0, 0));
            var overlappingTimeRange2 = new TimeRange(new TimeOnly(4, 0, 0), new TimeOnly(5, 0, 0));
            var overlappingTimeRange3 = new TimeRange(new TimeOnly(0, 30, 0), new TimeOnly(1, 0, 0));
            var overlappingTimeRange4 = new TimeRange(new TimeOnly(0, 30, 0), new TimeOnly(0, 45, 0));

            var result1 = timeRange.Overlaps(overlappingTimeRange1);
            var result2 = timeRange.Overlaps(overlappingTimeRange2);
            var result3 = timeRange.Overlaps(overlappingTimeRange3);
            var result4 = timeRange.Overlaps(overlappingTimeRange4);

            Assert.False(result1);
            Assert.False(result2);
            Assert.False(result3);
            Assert.False(result4);
        }

        [Fact]
        public void ReturnsTrueIfTimeRangeContainsTime()
        {
            var timeRange = new TimeRange(new TimeOnly(1, 0, 0), new TimeOnly(3, 0, 0));

            var result1 = timeRange.ContainsTime(new TimeOnly(1, 0, 0));
            var result2 = timeRange.ContainsTime(new TimeOnly(2, 0, 0));

            Assert.True(result1);
            Assert.True(result2);
        }

        [Fact]
        public void ReturnsFalseIfTimeRangeDoesNotContainTime()
        {
            var timeRange = new TimeRange(new TimeOnly(1, 0, 0), new TimeOnly(3, 0, 0));

            var result1 = timeRange.ContainsTime(new TimeOnly(0, 0, 0));
            var result2 = timeRange.ContainsTime(new TimeOnly(3, 0, 0));
            var result3 = timeRange.ContainsTime(new TimeOnly(5, 0, 0));

            Assert.False(result1);
            Assert.False(result2);
            Assert.False(result3);
        }

    }
}
