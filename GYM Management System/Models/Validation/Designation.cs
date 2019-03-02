using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GYM_Management_System.Models
{
    [MetadataType(typeof(MetadataDesignation))]
    public partial class Designation
    {
    }
    public class MetadataDesignation
    {
        [Required]
        //[MinLength(5, ErrorMessage = "User Designation Name can't be less than 5 characters")]
        //[MaxLength(100, ErrorMessage = "User Designation Name can't be more than 100 characters")]
        public string DesignationName { get; set; }
    }
}