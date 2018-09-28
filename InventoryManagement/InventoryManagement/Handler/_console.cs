using InventoryManagement.Model;
using System;
using System.IO;
using System.Linq;

namespace InventoryManagement.Handler
{
    public class _console
    {
        /// <summary>
        /// Called when application starts to display navigation - lines read from consoleOutput.txt
        /// </summary>
        public static void ConsoleNavigationOutput()
        {
            var directory = Path.GetFullPath(Environment.CurrentDirectory) + @"\consoleOutput.txt";
            using (var reader = new StreamReader(directory))
            {
                var line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }

        /// <summary>
        /// Console logs all of the items
        /// </summary>
        private static void DisplayItems()
        {
            using (var context = new Context())
            {
                Console.WriteLine("-LOADING-");
                Console.WriteLine();
                var items = context.Item.ToList();
                foreach (var item in items)
                {
                    Console.WriteLine($"| Item ID: {item.Id} | Name: {item.Name} | Item Quantity: {item.Quantity} | Description: {item.Description} |");
                }
            }
        }

        /// <summary>
        /// Checks input before calling _addItem function
        /// </summary>
        /// <param name="addItemFunctionNumber">1: AddItem will be called | 2: AddItemIfExists will be called</param>
        private static void _checkInputForAddingItem(int addItemFunctionNumber)
        {
            if (addItemFunctionNumber == 1) // add item
                Console.WriteLine("Will add a new item to the database OR will add the entered quantity to already existing item");
            else if (addItemFunctionNumber == 2) // add item or exists
                Console.WriteLine("Will add a new item to the database IF it does not already exist");

            Console.WriteLine("Do you want to proceed (Y/N)?");
            var input = Console.ReadLine();

            while (!(input.ToString() == "Y" || input.ToString() == "N"))
            {
                Console.WriteLine("Do you want to proceed (Y/N)?");
                input = Console.ReadLine();
            }

            if (input.ToString() == "Y")
            {
                _addItem(addItemFunctionNumber);

            }
            else if (input.ToString() == "N")
            {
                //will return back to CheckConsoleInput
                _consoleDetails();
            }
        }

        /// <summary>
        /// Function to handle adding items
        /// </summary>
        /// <param name="addItemFunction">1:AddItem will be called | 2:AddItemIfExists will be called</param>
        private static void _addItem(int addItemFunction)
        {
            Console.WriteLine();
            Console.WriteLine("Name: ");
            var name = Console.ReadLine();
            Console.WriteLine("Description: ");
            var description = Console.ReadLine();
            Console.WriteLine("Quantity: ");
            var quantity = Console.ReadLine();
            int resultQuantityTryParse;
            while (!(int.TryParse(quantity, out resultQuantityTryParse)))
            {
                Console.WriteLine("Quantity must be a number: ");
                quantity = Console.ReadLine();
            }

            Console.WriteLine("- Adding item -");

            //pass data to item handler
            if (addItemFunction == 1)
            {
                ItemHandler.AddItem(name, description, resultQuantityTryParse);
                _consoleDetails();
            }
            else if (addItemFunction == 2)
            {
                if (ItemHandler.AddItemOrExists(name, description, resultQuantityTryParse))
                {
                    _consoleDetails();
                }
                else
                {
                    _consoleDetails();
                    Console.WriteLine("Item already existed! Nothing was changed.");
                }
            }
        }

        /// <summary>
        /// Adjust the item quantity by item name
        /// </summary>
        private static void _adjustItemQuantityByName()
        {
            Console.WriteLine("Name: ");
            var name = Console.ReadLine();
            Console.WriteLine("Quantity: ");
            var quantity = Console.ReadLine();

            int resultQuantityTryParse;
            while (!(int.TryParse(quantity, out resultQuantityTryParse)))
            {
                Console.WriteLine("Quantity must be a number: ");
                quantity = Console.ReadLine();
            }

            ItemHandler.SetItemQuantityByName(name, resultQuantityTryParse);
            _consoleDetails();
        }

        /// <summary>
        /// Change the quantity of an item by the item ID
        /// </summary>
        private static void _adjustItemQuantityById()
        {
            Console.WriteLine("Id: ");
            var id = Console.ReadLine();
            Console.WriteLine("Quantity: ");
            var quantity = Console.ReadLine();

            int resultIdTryParse;
            while (!(int.TryParse(id, out resultIdTryParse)))
            {
                Console.WriteLine("Id must be a number: ");
                id = Console.ReadLine();
            }

            int resultQuantityTryParse;
            while (!(int.TryParse(quantity, out resultQuantityTryParse)))
            {
                Console.WriteLine("Quantity must be a number: ");
                quantity = Console.ReadLine();
            }

            ItemHandler.SetItemQuantityById(resultIdTryParse, resultQuantityTryParse);
            _consoleDetails();
        }

        /// <summary>
        /// Called after user input
        /// </summary>
        /// <param name="input">Input from user</param>
        public static bool CheckConsoleInput(string input)
        {
            switch (input)
            {
                case "1":
                    _consoleDetails();
                    DisplayItems();
                    return true;
                case "2":
                    _consoleDetails();
                    _checkInputForAddingItem(1);
                    return true;
                case "3":
                    _consoleDetails();
                    _checkInputForAddingItem(2);
                    return true;
                case "4":
                    _consoleDetails();
                    _adjustItemQuantityByName();
                    return true;
                case "5":
                    _consoleDetails();
                    _adjustItemQuantityById();
                    return true;
                case "Q":
                case "q":
                case "Quit":
                case "quit":
                case "QUIT":
                    Console.WriteLine("- Quitting -");
                    return false;
                default:
                    _consoleDetails();
                    Console.WriteLine("Unrecognised input");
                    return true;
            }
        }

        /// <summary>
        /// Clears console then shows the navigation
        /// </summary>
        private static void _consoleDetails()
        {
            Console.Clear();
            ConsoleNavigationOutput();
            Console.WriteLine();
        }
    }
}
