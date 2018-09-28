using InventoryManagement.Handler;
using System;

namespace InventoryManagement
{
    class Program
    {
        static void Main(string[] args)
        { 
            //display console navigation
            _console.ConsoleNavigationOutput();

            //check input until the user types one of the quit variables
            var input = _console.CheckConsoleInput(Console.ReadLine());
            while (input == true)
            {
                input = _console.CheckConsoleInput(Console.ReadLine());
            };
        }
    }
}
