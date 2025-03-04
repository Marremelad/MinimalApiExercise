using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalApiExercise.Models;

public class OrderProduct
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("Order")]
    public int OrderIdFk { get; set; }
    public Order? Order { get; set; }

    [ForeignKey("Product")]
    public int ProductIdFk { get; set; }
    public Product? Product { get; set; }
}