using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GYM_Management_System.Models
{
    [MetadataType(typeof(MetadataClient))]
    public partial class Client
    {
    }

    public class MetadataClient
        {
        [Required]
        [Display(Name ="Client Name")]
        public string ClietName { get; set; }
        [Required]
        [Display(Name = "Client ID")]
        public int ClientIdNumber { get; set; }
        [Required]
        [Display(Name = "Contact")]
        public string ClientContact { get; set; }
        [Required]
        [Display(Name = "E-mail")]
        
        public string ClientMail { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string ClientAddress { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public System.DateTime ClientGymStart { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string ClientUserName { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string ClientPassword { get; set; }
        [Required]
        [Display(Name = "Registration Fee")]
        public int ClientAdmitionfee { get; set; }
    }
}