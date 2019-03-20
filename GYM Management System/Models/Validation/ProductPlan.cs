using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GYM_Management_System.Models
{
    [MetadataType(typeof(MetadataProductPlan))]
    public partial class ProductPlan
    {
    }

    public class MetadataProductPlan 
    {
        [Required]
        [Display(Name ="Product Name")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Name is not correct format")]
        [MaxLength(50, ErrorMessage = "User name can't be more than 50 characters")]
        public string ProductName { get; set; }
    }
}