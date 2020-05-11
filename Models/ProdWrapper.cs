using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ECommerce.Models
{
    public class ProdWrapper
    {
        public string Search {get;set;}
        public Product Product {get;set;}

        public List<Product> AllProducts {get;set;}
    }
}