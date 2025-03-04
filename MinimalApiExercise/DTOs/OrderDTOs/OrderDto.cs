namespace MinimalApiExercise.DTOs.OrderDTOs;

public class OrderDto
{
    public int OrderId { get; set; }
    
    public DateOnly OrderDate { get; set; }
    
    public int CustomerId { get; set; }
    
    public IEnumerable<ProductDto>? OrderedProducts { get; set; }
}