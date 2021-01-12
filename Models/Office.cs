using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EFCAssets.Models
{
    public class Office
    {
        public int OfficeId { get; set; }
        [Display(Name ="Name")]
        public string OfficeName { get; set; }
        [Display(Name ="Country")]
        public string OfficeCountry { get; set; }
        public int CurrencyId { get; set; }
        [Display(Name ="Active")]
        public bool OfficeActive { get; set; }
        public Currency Currency { get; set; }

        public ICollection<Asset> Assets { get; set; }

    }
}
