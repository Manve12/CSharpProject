namespace InventoryManagement.Model
{
    /// <summary>
    /// Creates an instance of an item
    /// </summary>
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
