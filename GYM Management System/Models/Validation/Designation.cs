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
        public string DesignationName { get; set; }
    }
}