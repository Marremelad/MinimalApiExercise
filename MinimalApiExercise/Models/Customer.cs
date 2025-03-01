using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalApiExercise.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        public string? Phone { get; set; }
        
        public virtual List<Order>? Orders { get; set; }
    }
}
