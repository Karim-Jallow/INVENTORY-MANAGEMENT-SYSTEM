namespace INVENTORY_MANAGEMENT_SYSTEM.Models
{
    public class ProductReportViewModel
    {
        public string? ProductName { get; set; }
        public int CurrentStock { get; set; }
        public int StockBalance { get; set; }  // Adjust as needed
        public int ReorderLevel { get; set; }
        public List<StockMovement>? StockMovements { get; set; } // Added this
    }
}
