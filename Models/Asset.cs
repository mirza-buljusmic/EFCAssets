using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFCAssets.Models
{
    public class Asset
    {
        public int AssetId { get; set; }
        [Display(Name ="Asset Name")]
        public string AssetName { get; set; }
        [Display(Name ="Purchase Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AssetPurchaseDate { get; set; }
        [Display(Name ="Expiration Date")]
        public DateTime AssetExpirationDate { get; set; }
        [Display(Name ="Warning Date")]
        public DateTime AssetWarningDate { get; set; }
        
        [Required(ErrorMessage = "Price is required.")]
        [Display(Name ="Price")]
        public decimal AssetPrice { get; set; }
        [Display(Name ="Value")]
        public decimal AssetValue { get; set; }
        [Display(Name ="Active")]
        public bool AssetActive { get; set; }
        public int OfficeId { get; set; }
        public int CategoryId { get; set; }

        public Office Office { get; set; }
        public Category Category { get; set; }

    }
}
