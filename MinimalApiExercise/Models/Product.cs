﻿using System.ComponentModel.DataAnnotations;

namespace MinimalApiExercise.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public decimal ProductPrice { get; set; }
        
        public virtual List<OrderProduct>? OrderProducts { get; set; }
    }
}
