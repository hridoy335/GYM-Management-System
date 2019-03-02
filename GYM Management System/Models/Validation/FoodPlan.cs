using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GYM_Management_System.Models
{
    [MetadataType(typeof(MetadataFoodPlan))]
    public partial class FoodPlan
    {
    }

    public class MetadataFoodPlan
    {
        [Required]
        //[MinLength(5, ErrorMessage = "User Client Name can't be less than 5 characters")]
        //[MaxLength(20, ErrorMessage = "User Client Name can't be more than 20 characters")]
        [Display(Name ="Client Name")]
        public int ClientId { get; set; }

        [Required]

        [Display(Name ="Employee Name")]
        public int EmployeeId { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name ="Food Plan")]
        public string FoodPlan1 { get; set; }

        [Required]
        [Display(Name ="Update Date")]
        [DataType(DataType.Date)]
        public System.DateTime UpdateDate { get; set; }
    }
}