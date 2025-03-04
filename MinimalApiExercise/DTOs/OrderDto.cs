using MinimalApiExercise.Models;

namespace MinimalApiExercise.DTOs;

public class OrderDto
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }
    
    public DateOnly OrderDate { get; set; }
    
    public IEnumerable<ProductDto>? OrderedProducts { get; set; }
}