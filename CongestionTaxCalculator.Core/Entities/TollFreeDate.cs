using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Core.Entities
{
    public sealed class TollFreeDate
    {
        public TollFreeDate(int cityId, DateOnly date)
        {
            CityId = cityId;
            Date = date;
        }

        private TollFreeDate() { }

        public int Id { get; set; }

        public int CityId { get; private set; }

        public DateOnly Date { get; set; }
    }
}
