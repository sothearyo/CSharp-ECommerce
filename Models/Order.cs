using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ECommerce.Models
{
    public class Order
    {
        [Key]
        public int OrderId {get;set;}
    
        [Required]
        public int CustomerId {get;set;}
        [Required]
        public int ProductId {get;set;}

        [Required]
        [Range(0,Double.PositiveInfinity,ErrorMessage="Please enter a positive quantity")]
        public int OrderQty {get;set;}

        // naviation properties
        public Customer Customer {get;set;}
        public Product Product {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        // not mapped properties
        [NotMapped]
        public string TimeLongAgo
        {
            get
            {
                
                // int deltaYear = DateTime.Now.Year - CreatedAt.Year;
                // int deltaDay = DateTime.Now.DayOfYear - CreatedAt.DayOfYear;
                // int deltaSeconds = (DateTime.Now.Hour*3600 + DateTime.Now.Minute*60 + DateTime.Now.Second)- (CreatedAt.Hour*3600 + CreatedAt.Minute*60 + CreatedAt.Second);
                // int hoursAgo = deltaSeconds/60;

                // if (deltaYear != 0)
                // {
                //     return deltaYear;
                // }
                // else if (deltaDay != 0)
                // {
                //     return deltaDay;
                // }
                // else
                // {
                    
                //     return deltaMinutes;
                // }
                string TimeAgo = "";
                long elapsedTicks = DateTime.Now.Ticks - CreatedAt.Ticks;
                TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
                Console.WriteLine("   {0:N0} nanoseconds", elapsedTicks * 100);
                Console.WriteLine("   {0:N0} ticks", elapsedTicks);
                Console.WriteLine("   {0:N2} seconds", elapsedSpan.TotalSeconds);
                Console.WriteLine("   {0:N2} minutes", elapsedSpan.TotalMinutes);
                Console.WriteLine("   {0:N0} days, {1} hours, {2} minutes, {3} seconds",elapsedSpan.Days, elapsedSpan.Hours,elapsedSpan.Minutes, elapsedSpan.Seconds);
                
                if(elapsedSpan.Days != 0)
                {
                    TimeAgo = $"({elapsedSpan.Days} days ago)";
                }
                else if (elapsedSpan.Hours == 0)
                {
                    TimeAgo = $"({elapsedSpan.Minutes} minutes ago)";
                }
                else
                {
                    TimeAgo = $"({elapsedSpan.Hours} hours, {elapsedSpan.Minutes} min ago)";  
                }
                return TimeAgo;
                
            }
        }

    }
}