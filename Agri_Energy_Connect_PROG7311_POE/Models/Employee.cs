using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AgriEnergyConnectApp.Models
{
    public partial class Employee
    {
        [Required(ErrorMessage = "Please Enter Employee ID")]
        public string Employee_ID { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
    }
}
