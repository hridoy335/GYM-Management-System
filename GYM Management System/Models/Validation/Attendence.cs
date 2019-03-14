using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GYM_Management_System.Models
{
    [MetadataType(typeof(MetadataAttendence))]
    public partial class Attendence
    {

    }

    public class MetadataAttendence
    {
        [Required]
        [Display(Name = "Client Name")]
        public Nullable<int> ClientId { get; set; }

        [Required]
        [Display(Name = " Employee Name")]
        public Nullable<int> EmployeeId { get; set; }

        [Required]
        [Display(Name = "From Time")]
        [DataType(DataType.Time)]
        public Nullable<System.DateTime> FromTime { get; set; }

        //[Required]
        //[Display(Name = "To Time")]
        //// [DataType(DataType.Time)]
        //public Nullable<System.DateTime> ToTime { get; set; }

        [Required]
        [Display(Name = "Attendance Status")]
        public Nullable<bool> AttendenceStatus { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [Display(Name ="Attendance Date")]
        public Nullable<System.DateTime> AttendenceDate { get; set; }
    }
}