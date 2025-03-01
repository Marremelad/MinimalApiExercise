﻿namespace MinimalApiExercise.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int UserId { get; set; }
        public Customer Customer { get; set; }
    }
}
