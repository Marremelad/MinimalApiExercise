using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalApiExercise.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        
        public DateOnly OrderDate { get; set; }
        
        [ForeignKey("Customer")]
        
        public required int CustomerIdFk { get; set; }
        public Customer? Customer { get; set; }
        
        public virtual  List<OrderProduct>? OrderProducts { get; set; }
    }
}
