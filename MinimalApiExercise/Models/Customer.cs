using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalApiExercise.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        
        public required string CustomerFirstName { get; set; }

        public required string CustomerLastName { get; set; }

        [EmailAddress]
        public required string CustomerEmail { get; set; }

        public string? CustomerPhone { get; set; }
        
        public virtual List<Order>? Orders { get; set; }
    }
}
