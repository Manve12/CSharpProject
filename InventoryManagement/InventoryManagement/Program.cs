using InventoryManagement.Model;
using System;
using System.Linq;

namespace InventoryManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Data loading");
            
            foreach (var item in ItemHandler.GetItemsFromDatabase())
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine(ItemHandler.SetItemQuantityById(1,2));
            ItemHandler.AddItem("Cup", "Another cup", 1);
            Console.ReadLine();
        }
    }
}
