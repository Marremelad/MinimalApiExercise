using System.ComponentModel.DataAnnotations;

namespace MinimalApiExercise.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public decimal ProductPrice { get; set; }
    }
}
