using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ECommerce.Models
{
    public class CustWrapper
    {
        public string Search {get;set;}
        public Customer Customer {get;set;}

        public List<Customer> AllCustomers {get;set;}
    }
}