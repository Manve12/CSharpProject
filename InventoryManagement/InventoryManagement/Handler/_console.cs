using InventoryManagement.Exceptions;
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
        /// AddItem handler to call the correct _addItem method
        /// </summary>
        private static void AddItem()
        {
            Console.Clear();
            Console.WriteLine("1. Add a new item or adjust the existing item quantity");
            Console.WriteLine("2. Add a new item if it does not exist");
            Console.WriteLine("3. Return back to menu");
            var input = Console.ReadLine();
            int inputId;
            
            while (!(int.TryParse(input, out inputId)))
            {
                Console.WriteLine("Entered value must be of type integer");
            }

            try
            {
                if (inputId == 1 || inputId == 2)
                {
                    _addItem(inputId);
                }
                else if (inputId == 3)
                {
                    _consoleDetails();
                }
                else
                {
                    AddItem();
                }
            }
            catch (Exception ex)
            {
                AddItem();
                Console.WriteLine(ex.Message);
            }
        }
    
        /// <summary>
        /// Adjust item quantity by id
        /// </summary>
        /// <param name="id">ID of item</param>
        /// <param name="quantity">Quantity to set the item too</param>
        private static void _adjustItemQuantity(int id, int quantity)
        {
            if (ItemHandler.GetItemById(id) != null)
            {
                ItemHandler.SetItemQuantityById(id, quantity);
                _consoleDetails();
            }        
            else
            {
                throw new QuantityIdDoesNotExist("Item under that ID does not exist");
            }
        }

        /// <summary>
        /// Adjust item quantity by name
        /// </summary>
        /// <param name="name">Name of item</param>
        /// <param name="quantity">Quantity to set the item too</param>
        private static void _adjustItemQuantity(string name, int quantity)
        {
            if (ItemHandler.GetItemByName(name) != null)
            {
                ItemHandler.SetItemQuantityByName(name, quantity);
                _consoleDetails();
            }
            else
            {
                throw new QuantityNameDoesNotExist("Item under that Name does not exist");
            }
        }

        /// <summary>
        /// Adjust item quantity handler - works out which adjustItemQuantity method to call
        /// </summary>
        private static void AdjustItemQuantity()
        {
            Console.WriteLine("Enter Name or ID to adjust quantity of");
            string inputType = Console.ReadLine();

            Console.WriteLine("Enter new quantity:");
            string inputQuantity = Console.ReadLine();

            int quantity;
            while (!(int.TryParse(inputQuantity, out quantity)))
            {
                Console.WriteLine("Quantity must be of type integer");
                inputQuantity = Console.ReadLine();
            }

            //check if input is of integer type
            int id;
            var isId = int.TryParse(inputType, out id);

            try
            {
                //is id - call adjust item quantity by id
                if (isId)
                {
                    _adjustItemQuantity(id, quantity);
                }
                else // call adjust item quantity by name
                {
                    _adjustItemQuantity(inputType, quantity);
                }
                _consoleDetails();
            }
            catch (Exception ex) // handle the exceptions
            {
                _consoleDetails();
                Console.WriteLine(ex.Message); // output exception message
            }
        }

        /// <summary>
        /// Remove item by id
        /// </summary>
        /// <param name="id">ID of item</param>
        private static void _removeItem(int id)
        {
            ItemHandler.RemoveItemById(id);
        }

        /// <summary>
        /// Remove item by name
        /// </summary>
        /// <param name="name">Name of item</param>
        private static void _removeItem(string name)
        {
            ItemHandler.RemoveItemByName(name);
        }

        /// <summary>
        /// Remove Item handler - used to determine which _removeItem method to call
        /// </summary>
        private static void RemoveItem()
        {
            Console.WriteLine("WARNING: All of the quantity will be removed!");
            Console.WriteLine("");
            Console.WriteLine("Enter the Name or ID of the item to remove");
            var input = Console.ReadLine();

            int id;
            var isId = int.TryParse(input, out id);

            try
            {
                if (isId)
                {
                    _removeItem(id);
                }
                else
                {
                    _removeItem(input);
                }
                _consoleDetails();
            } catch (Exception ex)
            {
                _consoleDetails();
                Console.WriteLine(ex.Message);
            }
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
                    AddItem();
                    return true;
                case "3":
                    _consoleDetails();
                    AdjustItemQuantity();
                    return true;
                case "4":
                    _consoleDetails();
                    RemoveItem();
                    return true;
                case "Q":
                case "q":
                case "Quit":
                case "quit":
                case "QUIT":
                    _consoleDetails();
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
