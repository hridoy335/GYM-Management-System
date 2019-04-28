using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GYM_Management_System.Models
{
    [MetadataType(typeof(MetadataSell))]
    public partial class Sell
    {
    }

    public class MetadataSell
    {
     //   [Required]
        [Display(Name = "Product Name")]
        public int ProductPlanId { get; set; }
        [Required]
        [Display(Name ="Quantity")]
        public int ProductQuantity { get; set; }
        [Required]
        [Display(Name ="Total Amount")]
        public int TotalAmount { get; set; }
        //[Required]
        [Display(Name ="Employee Name")]
        public int EmployeeId { get; set; }
        //[Required]
        [Display(Name ="Client Name")]
        public int clientid { get; set; }
        [Required]
        [Display(Name ="Sell Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> SellDate { get; set; }
    }
}