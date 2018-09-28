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
            
            foreach (var item in Helper.GetItemsFromDatabase())
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine(Helper.SetItemQuantityById(1,2));
            Helper.AddItem("Cup", "Another cup", 1);
            Console.ReadLine();
        }
    }
}
