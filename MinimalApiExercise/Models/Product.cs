using System.ComponentModel.DataAnnotations;

namespace MinimalApiExercise.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [StringLength(35)]
        public required string Name { get; set; }
        
        [StringLength(300)]
        public string? Description { get; set; }

        public decimal Price { get; set; }
        
        public virtual List<OrderProduct>? OrderProducts { get; set; }
    }
}
