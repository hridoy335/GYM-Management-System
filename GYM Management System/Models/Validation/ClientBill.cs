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
        [Display(Name ="Client Name")]
        public int ClientId { get; set; }
        [Required]
        [Display(Name ="Bill Month")]
        [DataType(DataType.Date)]
        public System.DateTime BillMonth { get; set; }
        [Required]
        public string BillAmount { get; set; }
        [Required]
        public bool BillStatus { get; set; }
    }
}