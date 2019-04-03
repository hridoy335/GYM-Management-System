using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GYM_Management_System.Models.Validation
{
    [MetadataType(typeof(MetaClientBillTransection))]
    public partial class ClientBillTransection
    {
    }
    public class MetaClientBillTransection
    {
        [Required]
        [Display(Name ="Client Name")]
        public int ClientBillId { get; set; }
        [Required]
        [Display(Name ="Transection Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime TransectionDate { get; set; }
        [Required]
        [Display(Name ="Bill Month")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime BillMonth { get; set; }
        [Required]
        [Display(Name ="Amount")]
        public int Amount { get; set; }
        [Required]
        [Display(Name ="Bill Status")]
        public Nullable<bool> BillStatus { get; set; }
        [Required]
        [Display(Name ="Invoice Number")]
        public string InvoiceNumber { get; set; }
    }
}