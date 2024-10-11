using System.ComponentModel.DataAnnotations;

namespace INVENTORY_MANAGEMENT_SYSTEM.Models
{
    public class StockMovement
    {
        [Key] // This annotation is optional if the property is named Id
        public int Id { get; set; }  // Primary key

        public int ProductId { get; set; } // Foreign key to Product
        public int Quantity { get; set; }
        public string? MovementType { get; set; } // Sales, Swap, Replacement
        public DateTime MovementDate { get; set; }
        public string? EnteredBy { get; set; }

        // Navigation property (optional)
        public Product? Product { get; set; }
    }
}
