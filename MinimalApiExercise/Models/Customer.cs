using System.ComponentModel.DataAnnotations;
using MinimalApiExercise.ValidationAttributes;

namespace MinimalApiExercise.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        
        [StringLength(35)]
        public required string FirstName { get; set; }

        [StringLength(35)]
        public required string LastName { get; set; }

        [StringLength(254)]
        public required string Email { get; set; }
        
        [StringLength(12)]
        public string? Phone { get; set; }
        
        public virtual List<Order>? Orders { get; set; }
    }
}
