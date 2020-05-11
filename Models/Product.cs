using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ECommerce.Models
{
    public class Product
    {
        [Key]
        public int ProductId {get;set;}

        [Required(ErrorMessage="Please enter name")]
        [MinLength(2,ErrorMessage="Must be at least 2 characters")]
        [MaxLength(15,ErrorMessage="Must be less than 15 characters")]
        public string ProductName {get;set;}
        
        public string ProductImg {get;set;}

        [Required(ErrorMessage="Please enter a description")]
        public string ProductDesc {get;set;}

        [Range(0,Double.PositiveInfinity,ErrorMessage="Please enter a positive quantity")]
        public int ProductQty {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        // navigation properties
        public List<Order> AllOrders {get;set;}

    }

}