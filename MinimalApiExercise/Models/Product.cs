using System.ComponentModel.DataAnnotations;

namespace MinimalApiExercise.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        
        public virtual List<OrderProduct>? OrderProducts { get; set; }
    }
}
