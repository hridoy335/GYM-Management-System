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
        [Display(Name ="Product Name")]
        public string ExpenseProductName { get; set; }
        [Required]
        [Display(Name ="Product Quantity")]
        public string ExpenseProductQuantity { get; set; }
        [Required]
        [Display(Name ="Product Price")]
        public int ExpenseProductAmount { get; set; }
        [Required]
        [Display(Name ="Expense Buy Datess")]
        [DataType(DataType.Date)]
        public System.DateTime ExpenseBuyDate { get; set; }
        [Required]
        [Display(Name ="Employee Name")]
        public int EmployeeId { get; set; }
    }
}