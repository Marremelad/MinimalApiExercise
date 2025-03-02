using System.ComponentModel.DataAnnotations;
using MinimalApiExercise.ValidationAttributes;

namespace MinimalApiExercise.DTOs.CustomerDTOs;

public class CustomerCreateDto
{
    [Required]
    [StringLength(35)]
    public required string FirstName { get; set; }

    [Required]
    [StringLength(35)]
    public required string LastName { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(254)]
    public required string Email { get; set; }
        
    [Required]
    [SwedishPhoneNumber(ErrorMessage = "Pleaser enter a Swedish phone number")]
    [StringLength(12)]
    public string? Phone { get; set; }
}