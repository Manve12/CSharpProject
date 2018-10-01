using System;

namespace InventoryManagement.Exceptions
{
    public class AddItem : Exception
    {
        public AddItem() : base() { }
        public AddItem(string message) : base(message) { }
    }

    public class Quantity : Exception
    {
        public Quantity() : base() { }
        public Quantity(string message) : base(message) { }
    }
    public class QuantityNameDoesNotExist : Exception
    {
        public QuantityNameDoesNotExist() : base() { }
        public QuantityNameDoesNotExist(string message) : base(message) { }
    }
    public class QuantityIdDoesNotExist : Exception
    {
        public QuantityIdDoesNotExist() : base() { }
        public QuantityIdDoesNotExist(string message) : base(message) { }
    }

    public class RemoveItem : Exception
    {
        public RemoveItem() : base() { }
        public RemoveItem(string message) : base(message) { }
    }
    public class RemoveItemIdDoesNotExist : Exception
    {
        public RemoveItemIdDoesNotExist() : base() { }
        public RemoveItemIdDoesNotExist(string message) : base(message) { }
    }
    public class RemoveItemNameDoesNotExist : Exception
    {
        public RemoveItemNameDoesNotExist() : base() { }
        public RemoveItemNameDoesNotExist(string message) : base(message) { }
    }

}
