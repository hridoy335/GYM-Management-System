using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GYM_Management_System.Models
{
    [MetadataType(typeof(MetadataClientBill))]
    public partial class ClientBill
    {
    }
    public class MetadataClientBill
    {
        [Required]
        //[MinLength(5, ErrorMessage = "User Client Name can't be less than 5 characters")]
        //[MaxLength(20, ErrorMessage = "User Client Name can't be more than 20 characters")]
        [Display(Name ="Client Name")]
        public int ClientId { get; set; }

        [Required]
        [Display(Name ="Bill Month")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime BillMonth { get; set; }

        [Required]
        //[MinLength(3, ErrorMessage = "User Bill Amount can't be less than 3 characters")]
        //[MaxLength(5, ErrorMessage = "User Bill Amount can't be more than 5 characters")]
        public string BillAmount { get; set; }

        [Required]
        [Display(Name ="Bill Status")]
        public bool BillStatus { get; set; }
    }
}