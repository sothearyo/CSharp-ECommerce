using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ECommerce.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId {get;set;}
        [Required]
        public string CustomerName {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        //navigation properties
        public List<Order> AllOrders {get;set;}
    }
}