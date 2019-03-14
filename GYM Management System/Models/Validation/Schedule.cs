using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GYM_Management_System.Models
{
    [MetadataType(typeof(MetadataSchedule))]
    public partial class Schedule
    {
    }
    public class MetadataSchedule
    {
        [Required]

        public int ScheduleTimeId { get; set; }
        [Required]
        [Display(Name ="Client Name")]
        public int ClientId { get; set; }
        [Required]
        [Display(Name ="Employee Name")]
        public int EmployeeId { get; set; }
    }
}