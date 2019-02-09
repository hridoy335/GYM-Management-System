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
        public string EmployeeName { get; set; }
        [Required]
        public int DesignationId { get; set; }
        [Required]
        public int Employee_ID { get; set; }
        [Required]
        public string Employee_Contact { get; set; }
        [Required]
        public string Employee_Mail { get; set; }
        [Required]
        public string Employee_Address { get; set; }
        [Required]
        public string Employe_UserName { get; set; }
        [Required]
        public string Employee_Password { get; set; }
    }
}