using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AgriEnergyConnectApp.Models
{
    public partial class Product
    {
        public int Product_ID { get; set; }
        [Required(ErrorMessage = "Please Enter Product Name")]
        public string Product_Name { get; set; }
        [Required(ErrorMessage = "Please Enter Product Price")]
        public int Product_Price { get; set; }
        [Required(ErrorMessage = "Please Enter Product Quantity")]
        public int Product_Quantity { get; set; }
        [Required(ErrorMessage = "Please Enter Product Type")]
        public string Product_Type { get; set; }
        [Required(ErrorMessage = "Please Enter Date")]
        [DataType(DataType.Date)]
        public DateTime Product_Date { get; set; }

        public string Farmer_ID { get; set; }

        public virtual Farmer Farmer { get; set; }
    }
}
