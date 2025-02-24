using System.ComponentModel.DataAnnotations;

namespace MinimalApiExercise.Models;

public class Service
{
    [Key]
    public int ServiceId { get; set; }

    [StringLength(35)]
    public required string ServiceName { get; set; }

    [StringLength(200)]
    public string? ServiceDescription { get; set; }

    public decimal ServicePrice { get; set; }
}