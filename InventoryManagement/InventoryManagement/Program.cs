using InventoryManagement.Handler;
using InventoryManagement.Model;
using System;
using System.Linq;

namespace InventoryManagement
{
    class Program
    {
        static void Main(string[] args)
        { 
            _console.ConsoleNavigationOutput();

            var input = _console.CheckConsoleInput(Console.ReadLine());
            while (input == true)
            {
                input = _console.CheckConsoleInput(Console.ReadLine());
            };
        }
    }
}
