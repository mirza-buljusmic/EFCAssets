using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EFCAssets.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Display(Name ="Name")]
        public string CategoryName { get; set; }
        [Display(Name ="EOL Months")]
        public int CategoryEOLMonths { get; set; }
        [Display(Name ="Active")]
        public bool CategoryActive { get; set; }

        public ICollection<Asset> Assets { get; set; }
    }
}
