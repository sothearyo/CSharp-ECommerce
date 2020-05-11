using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ECommerce.Models
{
    public class OrdersWrapper
    {
        public Order Order {get;set;}
        public string Search {get;set;}
        public List<Customer> AllCustomers {get;set;}
        public List<Product> AllProducts {get;set;}
        public List<Order> AllOrders {get;set;}
    }
}