using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTestActions.Test
{
    public abstract class Item
    {
        public float Duration { get; set; }
        public ItemType Type { get; set; }
        public ActionType Action { get; set; }
        public int Order { get; set; }
        public ItemStatus Status { get; set; }
        public string Name { get; protected set; }

        public Item(ActionType action)
        {
            Status = ItemStatus.Created;
            Duration = 0;
            Name = "NonameItem";
            Action = action;
        }

        public abstract ItemStatus Run();
    }
}
