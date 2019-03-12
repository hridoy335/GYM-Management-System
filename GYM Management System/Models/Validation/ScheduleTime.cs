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
        //[MaxLength(100, ErrorMessage = "User Schedule Name can't be more than 100 characters")]
        public string ScheduleName { get; set; }
        [Required]
        [DataType(DataType.Time)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public System.DateTime StartTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public System.DateTime EndTime { get; set; }
    }
}