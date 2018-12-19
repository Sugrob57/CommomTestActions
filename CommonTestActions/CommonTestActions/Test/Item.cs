using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTestActions.Test
{
    public abstract class Item
    {
        public float Duration { get; set; }
        public ItemType Type { get; set; }
        
        public int Order { get; set; }
        public ItemStatus Status { get; set; }
        public string Name { get; set; }

        public Item()
        {
            Status = ItemStatus.Created;
            Duration = 0;
            Name = "NonameItem";         
        }

        public abstract ItemStatus Run();

        public virtual void PrintResults()
        {
            Console.WriteLine("{0} \"{1}\" has Status \"{2}, Order {3}, Duration(ms): {4}", Type, Name, Status, Order, Duration);
        }
    }
}
