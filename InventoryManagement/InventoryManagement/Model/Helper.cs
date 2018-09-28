using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Model
{
    public class Helper
    {
        /// <summary>
        /// Returns item by id
        /// </summary>
        /// <param name="id">ID of the item</param>
        /// <returns>Item or null</returns>
        public static Item GetItemById(int id)
        {
            using (var context = new Context())
            {
                var item = context.Item.FirstOrDefault(i => i.Id == id);
                if (item != null)
                {
                    return item;
                } else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Returns item by name
        /// </summary>
        /// <param name="name">Name of item</param>
        /// <returns>Item</returns>
        public static Item GetItemByName(string name)
        {
            using (var context = new Context())
            {
                var item = context.Item.FirstOrDefault(i => i.Name.ToLower() == name.ToLower()); // to lower is used to make sure that all instances are checked the same
                if (item != null)
                {
                    return item;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Returns list of items
        /// </summary>
        /// <returns>List of Item</returns>
        public static List<Item> GetItemsFromDatabase()
        {
            using (var context = new Context())
            {
                return context.Item.ToList();
            }
        }

        /// <summary>
        /// Sets items quantity by the id
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <param name="quantity">Quantity to set to</param>
        /// <returns>Boolean</returns>
        public static bool SetItemQuantityById(int id, int quantity)
        {
            var itemById = GetItemById(id); // get the item
            if (itemById != null) // check if item exists
            {
                //set item quantity
                using (var context = new Context())
                {
                    context.Item.Attach(itemById);
                    itemById.Quantity = quantity;
                    var itemQuantityEntry = context.Entry(itemById);
                    itemQuantityEntry.State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Sets item quantity by name
        /// </summary>
        /// <param name="name">Name of item</param>
        /// <param name="quantity">Quantity to add</param>
        /// <returns>Boolean</returns>
        public static bool SetItemQuantityByName(string name, int quantity)
        {
            var itemByName = GetItemByName(name); // get the item
            if (itemByName != null) // check if item exists
            {
                //set item quantity
                using (var context = new Context())
                {
                    context.Item.Attach(itemByName);
                    itemByName.Quantity = quantity;
                    var itemQuantityEntry = context.Entry(itemByName);
                    itemQuantityEntry.State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Adds item or quantity to already existing item
        /// </summary>
        /// <param name="name">Name of item</param>
        /// <param name="description">Description of item</param>
        /// <param name="quantity">Quantity of item</param>
        public static void AddItem(string name, string description, int quantity)
        {
            using (var context = new Context())
            {
                //check if item exists by the name
                var itemByName = GetItemByName(name);
                if (itemByName != null) // item exists - add 1 to quantity
                {
                    SetItemQuantityByName(name, itemByName.Quantity + quantity);
                }
                else // item does not exist - add to database
                {
                    _addItem(context, name, description, quantity);
                }
            }
        }


        /// <summary>
        /// Adds an item if it does not exist
        /// </summary>
        /// <param name="name">Name of item</param>
        /// <param name="description">Description of item</param>
        /// <param name="quantity">Quantity of item</param>
        /// <returns>True if added | False if exists</returns>
        public static bool AddItemOrExists(string name, string description, int quantity)
        {
            using (var context = new Context())
            {
                var itemByName = GetItemByName(name);
                if (itemByName == null)
                {
                    _addItem(context, name, description, quantity);
                    return true;
                }
                return false;
            }
        }

        ///PRIVATES///

        private static void _addItem(Context context, string name, string description, int quantity)
        {
            context.Item.Add(
                new Item()
                {
                    Name = name,
                    Description = description,
                    Quantity = quantity
                }
            );
            context.SaveChanges();
        }
    }
}
