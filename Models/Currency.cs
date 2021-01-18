using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace EFCAssets.Models
{
    public class Currency
    {
        public int Id { get; set; }
        [Display(Name ="Name")]
        public string CurrencyName { get; set; }
        [Display(Name ="Exchange rate")]
        public decimal CurrensyToUSD { get; set; }
        [StringLength (50)]
        [Display(Name ="Comment")]
        public string CurrencyComment { get; set; }
        [Display(Name ="Active")]
        public bool CurrencyActive { get; set; }
        public ICollection<Office> Offices { get; set; }
    }
}
