using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GYM_Management_System.Models
{
    [MetadataType(typeof(MetadataEmployee))]
    public partial class Employee
    {
    }
    public class MetadataEmployee
    {
        [Required]
        //[MinLength(5, ErrorMessage = "User Name can't be less than 5 characters")]
        [MaxLength(50, ErrorMessage = "Name can't be more than 50 characters")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Name is not correct format")]
        [Display(Name ="Employee Name")]
        public string EmployeeName { get; set; }

        [Required]
        //[MinLength(3, ErrorMessage = "User Designation Name can't be less than 3 characters")]
        //[MaxLength(10, ErrorMessage = "User Designation Name can't be more than 10 characters")]
        [Display(Name ="Employee Designation")]
        public int DesignationId { get; set; }

        [Required]
        //[MinLength(3, ErrorMessage = "User Employee ID can't be less than 3 characters")]
        //[MaxLength(10, ErrorMessage = "User Employee ID can't be more than 10 characters")]
        [Display(Name ="Employee ID")]
        public int Employee_ID { get; set; }

        [Required]
        //[MinLength(6, ErrorMessage = "User Contact can't be less than 6 characters")]
        [MaxLength(20, ErrorMessage = "Contact can't be more than 20 characters")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\d+", ErrorMessage = "Name is not correct format")]
        [Display(Name ="Contact")]
        public string Employee_Contact { get; set; }

        [Required]
        //[MinLength(12, ErrorMessage = "User E-mail can't be less than 12 characters")]
        [MaxLength(100, ErrorMessage = "E-mail can't be more than 100 characters")]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="E-mail")]
        public string Employee_Mail { get; set; }

        [Required]
        //[MinLength(10, ErrorMessage = "User Address can't be less than 10 characters")]
        [MaxLength(100, ErrorMessage = "Address can't be more than 100 characters")]
        [Display(Name ="Address")]
        public string Employee_Address { get; set; }

        [Required]
        //[MinLength(10, ErrorMessage = "User User Name can't be less than 10 characters")]
        [MaxLength(50, ErrorMessage = "User Name can't be more than 50 characters")]
        [Display(Name ="Username")]
        public string Employe_UserName { get; set; }

        [Required]
        //[MinLength(4, ErrorMessage = "User Password can't be less than 4 characters")]
        [MaxLength(50, ErrorMessage = "Password can't be more than 50 characters")]
        [DataType(DataType.Password)]
        [Display(Name ="Password")]
        public string Employee_Password { get; set; }
    }
}