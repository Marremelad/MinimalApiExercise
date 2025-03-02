using System.ComponentModel.DataAnnotations;
using MinimalApiExercise.ValidationAttributes;

namespace MinimalApiExercise.DTOs;

public class CustomerDto
{
    public int CustomerId { get; set; }
        
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
    
}