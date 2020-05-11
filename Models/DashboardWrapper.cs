using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ECommerce.Models
{
    public class DashboardWrapper
    {
        public string Search {get;set;}

        public List<Product> RecentProducts {get;set;}
        public List<Customer> RecentCustomers {get;set;}
        public List<Order> RecentOrders {get;set;}
    }
}