using System.Data.Entity;
using InventoryManagement.Model;
using System;
using System.Collections.Generic;

namespace InventoryManagement.Data
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        //Change the seed of the database
        protected override void Seed(Context context)
        {
            //create all of the items
            var whiteboard = new Item()
            {
                Name = "Whiteboard",
                Description = "A simple whiteboard",
                Quantity = 1
            };

            var desk = new Item()
            {
                Name = "Wood desk",
                Description = "Wooden desk",
                Quantity = 1
            };

            var cup = new Item()
            {
                Name = "Cup",
                Description = "Single cup",
                Quantity = 1
            };

            //add the items to the database
            context.Item.Add(whiteboard);
            context.Item.Add(desk);
            context.Item.Add(cup);

            context.SaveChanges();
        }
    }
}
