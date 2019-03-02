using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GYM_Management_System.Models
{
    [MetadataType(typeof(MetadataServicess))]
    public partial class Servicess
    {
    }

    public class MetadataServicess
    {
        [Required]
        //[MinLength(5, ErrorMessage = "User Service Name can't be less than 5 characters")]
        //[MaxLength(100, ErrorMessage = "User Service Name can't be more than 100 characters")]
        [Display(Name ="Service Name")]
        public string ServiceName { get; set; }

        [Required]
        //[MaxLength(2, ErrorMessage = "User Service Day can't be more than 2 characters")]
        [Display(Name ="Service Day")]
        public string ServiceDay { get; set; }

        [Required]
        //[MaxLength(5, ErrorMessage = "User Service Price can't be more than 5 characters")]
        [Display(Name ="Service Price")]
        public int ServieAmount { get; set; }
    }
}