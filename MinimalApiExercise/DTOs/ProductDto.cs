namespace MinimalApiExercise.DTOs;

public class ProductDto
{
    public int ProductId { get; set; }
    
    public required string ProductName { get; set; }
    
    public string? ProductDescription { get; set; }

    public decimal ProductPrice { get; set; }
}