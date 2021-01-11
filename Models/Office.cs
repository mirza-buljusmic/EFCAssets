using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCAssets.Models
{
    public class Office
    {
        public int OfficeId { get; set; }
        public string OfficeName { get; set; }
        public string OfficeCountry { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public ICollection<Asset> Assets { get; set; }

    }
}
