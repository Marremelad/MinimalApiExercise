using System.ComponentModel.DataAnnotations;

namespace MinimalApiExercise.Models;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [StringLength(35)]
    public required string CustomerName { get; set; }

    [StringLength(50)]
    public required string CustomerEmail { get; set; }

    [RegularExpression("^\\+46\\s?\\d{3}\\s?\\d{2}\\s?\\d{2}\\s?\\d{2}$|^0\\d{3}\\s?\\d{2}\\s?\\d{2}\\s?\\d{2}$\n")]
    [StringLength(16, MinimumLength = 10)]
    public required string CustomerPhoneNumber { get; set; }
}