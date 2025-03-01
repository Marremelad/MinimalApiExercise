using System.ComponentModel.DataAnnotations;

namespace MinimalApiExercise.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public required string Name { get; set; }
        
        [StringLength(300)]
        public string? Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

        
        public virtual List<OrderProduct>? OrderProducts { get; set; }
    }
}
