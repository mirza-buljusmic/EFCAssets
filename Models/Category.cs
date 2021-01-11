using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCAssets.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int CategoryEOLMonths { get; set; }

        public ICollection<Asset> Assets { get; set; }
    }
}
