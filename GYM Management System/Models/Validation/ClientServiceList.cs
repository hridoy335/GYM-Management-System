using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GYM_Management_System.Models
{
    [MetadataType(typeof(MetadataClientServiceList))]
    public partial class ClientServiceList
    {
    }

    public class MetadataClientServiceList
    {
        [Required]
        [Display(Name ="Client Name")]
        public int ClientId { get; set; }
        [Required]
        [Display(Name ="Service Name")]
        public int ServiceId { get; set; }
    }
}