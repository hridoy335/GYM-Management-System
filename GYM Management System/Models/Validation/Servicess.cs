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
        [Display(Name ="Service Name")]
        public string ServiceName { get; set; }
        [Required]
        [Display(Name ="Service Day")]
        public string ServiceDay { get; set; }
        [Required]
        [Display(Name ="Service Price")]
        public int ServieAmount { get; set; }
    }
}