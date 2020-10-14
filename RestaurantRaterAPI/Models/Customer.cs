using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantRaterAPI.Models
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        private string FullName { set { FullName = FirstName + " " + LastName; } }

        public int Age { get; set; }


    }
}