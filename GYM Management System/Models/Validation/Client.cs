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
        public string ClietName { get; set; }
        [Required]
        public int ClientIdNumber { get; set; }
        [Required]
        public string ClientContact { get; set; }
        [Required]
        public string ClientMail { get; set; }
        [Required]
        public string ClientAddress { get; set; }
        [Required]
        public System.DateTime ClientGymStart { get; set; }
        [Required]
        public string ClientUserName { get; set; }
        [Required]
        public string ClientPassword { get; set; }
        [Required]
        public int ClientAdmitionfee { get; set; }
    }
}