using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AgriEnergyConnectApp.Models
{
    public partial class Farmer
    {
        public Farmer()
        {
            Products = new HashSet<Product>();
        }
        [Required(ErrorMessage = "Please Enter Farmer ID")]
        public string Farmer_ID { get; set; }
        [Required(ErrorMessage = "Please Enter Farmer Name")]
        public string FarmerName { get; set; }
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
