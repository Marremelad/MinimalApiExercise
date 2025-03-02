using MinimalApiExercise.Models;

namespace MinimalApiExercise.DTOs;

public class OrderDto
{
    public int OrderId { get; set; }

    public IEnumerable<ProductDto>? OrderedProducts { get; set; }
}