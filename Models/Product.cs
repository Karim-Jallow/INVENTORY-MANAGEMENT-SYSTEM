namespace INVENTORY_MANAGEMENT_SYSTEM.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string?Description { get; set; }
        public string?Category { get; set; }
        public string?Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? EnteredBy { get; set; }
        public int ReorderLevel { get; set; }
        public int CurrentStock { get; set; }
          public int CalculateStockBalance(List<StockMovement> movements)
    {
        // Assume movements is a list of stock transactions that affect stock
        int stockBalance = CurrentStock;

        foreach (var movement in movements)
        {
            stockBalance += movement.Quantity; // Assuming Quantity is positive for stock addition and negative for stock reduction
        }

        return stockBalance;
    }
    }
}
