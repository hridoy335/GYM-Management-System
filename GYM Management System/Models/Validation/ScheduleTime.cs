using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GYM_Management_System.Models
{
    [MetadataType(typeof(MetadataScheduleTime))]
    public partial class ScheduleTime
    {
    }

    public class MetadataScheduleTime
    {
        [Required]
        //[MinLength(5, ErrorMessage = "User Schedule Name can't be less than 5 characters")]
        [MaxLength(50, ErrorMessage = "User Schedule Name can't be more than 50 characters")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Name is not correct format")]
        [Display(Name ="Schedule Name")]
        public string ScheduleName { get; set; }
        [Required]
        [DataType(DataType.Time)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [Display(Name ="Start Time")]
        public System.DateTime StartTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [Display(Name ="End Time")]
        public System.DateTime EndTime { get; set; }
    }
}