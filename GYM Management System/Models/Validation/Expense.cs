using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GYM_Management_System.Models
{
    [MetadataType(typeof(MetadataExpense))]
    public partial class Expense
    {
    }
    public class MetadataExpense
    {
        [Required]
        //[MinLength(3, ErrorMessage = "User Name can't be less than 3 characters")]
        //[MaxLength(100, ErrorMessage = "User Name can't be more than 100 characters")]
        [Display(Name ="Product Name")]
        public string ExpenseProductName { get; set; }

        [Required]
        //[MinLength(1, ErrorMessage = "User Product Quantity can't be less than 1 characters")]
        //[MaxLength(3, ErrorMessage = "User Product Quantity can't be more than 3 characters")]
        [Display(Name ="Product Quantity")]
        public string ExpenseProductQuantity { get; set; }

        [Required]
        //[MinLength(1, ErrorMessage = "User Product Price can't be less than 1 characters")]
        //[MaxLength(4, ErrorMessage = "User Product Price can't be more than 4 characters")]
        [Display(Name ="Product Price")]
        public int ExpenseProductAmount { get; set; }

        [Required]
        [Display(Name ="Expense Buy Datess")]
        [DataType(DataType.Date)]
       // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime ExpenseBuyDate { get; set; }

        [Required]
        [Display(Name ="Employee Name")]
        public int EmployeeId { get; set; }
    }
}