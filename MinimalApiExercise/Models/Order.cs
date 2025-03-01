using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalApiExercise.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        
        public DateOnly OrderDate { get; set; }
        
        [ForeignKey("Customer")]
        public int CustomerIdFk { get; set; }
        public required Customer Customer { get; set; }
        
        public virtual required List<OrderProduct> OrderProducts { get; set; }
    }
}
