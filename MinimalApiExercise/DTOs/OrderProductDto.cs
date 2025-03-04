using System.ComponentModel.DataAnnotations;

namespace MinimalApiExercise.DTOs;

public class OrderProductDto
{
    [Required]
    public int OrderId { get; set; }

    [Required]
    public int ProductId { get; set; }
}