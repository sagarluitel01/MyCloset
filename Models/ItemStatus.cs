namespace MyCloset.Models
{
    public  class ItemStatus
    {
        public Guid StatusId { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    } 
}