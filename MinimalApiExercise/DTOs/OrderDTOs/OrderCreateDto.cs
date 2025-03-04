using System.ComponentModel.DataAnnotations;

namespace MinimalApiExercise.DTOs.OrderDTOs;

public class OrderCreateDto
{
    [Required]
    public required int CustomerId { get; set; }

    [Required]
    public required List<string> Items { get; set; }
}