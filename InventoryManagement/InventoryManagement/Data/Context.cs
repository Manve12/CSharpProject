using InventoryManagement.Data;
using System.Data.Entity;

namespace InventoryManagement.Model
{
    public class Context : DbContext
    {
       // public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Item> Item { get; set; } 

        public Context()
        {
            //use a custom database initializer
            Database.SetInitializer(new DatabaseInitializer());
        }
    }
}
