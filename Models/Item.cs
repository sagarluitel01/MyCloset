namespace MyCloset.Models
{
    public class Item
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public DateTime UpdateDateTime { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public int TimeWorn { get; set; } // Number of times the item has been worn 
        public ItemStatus Status { get; set; } = new ItemStatus();
    }
}