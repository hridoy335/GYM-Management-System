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
        [Display(Name ="Total Name")]
        public int TotalAmount { get; set; }
        [Required]
  //      [Display(Name ="Employee Name")]
        public int EmployeeId { get; set; }
        [Required]
     //   [Display(Name ="Client Name")]
        public int clientid { get; set; }
    }
}