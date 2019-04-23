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
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage  = "Name is not correct format")]

        //[MinLength(5, ErrorMessage = "User Name can't be less than 5 characters")]
        [MaxLength(50, ErrorMessage = "User name can't be more than 50 characters")]
        [Display(Name = "Client Name")]
        public string ClietName { get; set; }

        [Required]
        //[MinLength(3, ErrorMessage="User ID can't be less than 3 characters")]
        //[MaxLength(10, ErrorMessage = "User ID can't be more than 10 characters")]
        [Display(Name = "Client ID")]
        
        public int ClientIdNumber { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "User Contact can't be less than 6 characters")]
        [MaxLength(11, ErrorMessage = "User Contact can't be more than 11 characters")]
        [Display(Name = "Contact")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\d+", ErrorMessage = "Name is not correct format")]
        public string ClientContact { get; set; }

        [Required]
        //[MinLength(12, ErrorMessage = "User E-mail can't be less than 12 characters")]
        [MaxLength(100, ErrorMessage = "User E-mail can't be more than 100 characters")]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string ClientMail { get; set; }

        [Required]
        //[MinLength(10, ErrorMessage = "User Address can't be less than 10 characters")]
        [MaxLength(100, ErrorMessage = "User Address can't be more than 100 characters")]
        [Display(Name = "Address")]
        public string ClientAddress { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime ClientGymStart { get; set; }

        [Required]
        //[MinLength(10, ErrorMessage = "User User Name can't be less than 10 characters")]
        [MaxLength(50, ErrorMessage = "User User Name can't be more than 50 characters")]
        [Display(Name = "Username")]
        public string ClientUserName { get; set; }

        [Required]
        //[MinLength(4, ErrorMessage = "User Password can't be less than 10 characters")]
        [MaxLength(50, ErrorMessage = "User Password can't be more than 50 characters")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string ClientPassword { get; set; }

        [Required]
        [Display(Name = "Registration Fee")]
        //[MinLength(3, ErrorMessage = "User Registration Free can't be less than 3 characters")]
        //[MaxLength(5, ErrorMessage = "User Registration Free can't be more than 5 characters")]
        public int ClientAdmitionfee { get; set; }
     
}
}