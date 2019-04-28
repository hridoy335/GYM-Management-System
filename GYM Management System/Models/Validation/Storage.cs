using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GYM_Management_System.Models
{
    [MetadataType(typeof(MetadataStorage))]
    public partial class Storage
    {
    }
    public class MetadataStorage
    {
        [Display(Name ="Product Name")]
        public int ProductPlanId { get; set; }
        [Display(Name = "Product Quantity")]
        public int ProductQuantity { get; set; }
        [Display(Name = "Expire Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public System.DateTime ProductExpireDate { get; set; }
    }
}