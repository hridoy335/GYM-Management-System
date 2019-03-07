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
        //[MinLength(5, ErrorMessage = "User Client Name can't be less than 5 characters")]
        //[MaxLength(20, ErrorMessage = "User Client Name can't be more than 20 characters")]
        [Display(Name ="Client Name")]
        public int ClientId { get; set; }

        //[MinLength(10, ErrorMessage = "User Service Name can't be less than 10 characters")]
        //[MaxLength(30, ErrorMessage = "User Service name can't be more than 30 characters")]
        [Required]
        [Display(Name ="Service Name")]
        public int ServiceId { get; set; }

    }
}