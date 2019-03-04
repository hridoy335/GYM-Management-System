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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public System.DateTime UpdateDate { get; set; }
    }
}