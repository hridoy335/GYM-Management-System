using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GYM_Management_System.Models
{
    [MetadataType(typeof(MetadataProductBuying))]
    public partial class ProductBuying
    {
    }
    public class MetadataProductBuying
    {
        [Required]
        [Display(Name ="Product Name")]
        public int ProductPlanId { get; set; }

        [Required]
        [Display(Name ="Quantity")]
        public int ProductQuantity { get; set; }

        [Required]
        [Display(Name ="Buying Date")]
        [DataType(DataType.Date)]
        public System.DateTime ProdyctBuyingDate { get; set; }

        [Required]
        [Display(Name ="Expire Date")]
        [DataType(DataType.Date)]
        public System.DateTime ProductExpireDate { get; set; }

        [Required]
        [Display(Name ="Buy Par Price")]
        public int ProductBuyParPrice { get; set; }

        [Required]
        [Display(Name ="Sell Par Price")]
        public int productSellParPrice { get; set; }

        [Required]
        [Display(Name ="Total Ammount")]
        public int TotalAmount { get; set; }

        [Required]
        [Display(Name ="Employee Name")]
        public int EmployeeId { get; set; }
    }
}