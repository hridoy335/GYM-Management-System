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
        public string ScheduleName { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public System.DateTime StartTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public System.DateTime EndTime { get; set; }
    }
}