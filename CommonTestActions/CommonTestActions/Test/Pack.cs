using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CommonTestActions.Test
{
    public class Pack : Item
    {
        public List<Item> Items { get; set; }
        public Dictionary<string, string> Responses { get; set; }

        public Pack(string name) : base()
        {
            if (name.Length > 0)
                base.Name = name;
            //TODO check name for unique
            else
                throw new ArgumentNullException();

            base.Type = ItemType.Pack;

            Items = new List<Item>();
            Responses = new Dictionary<string, string>();
        }

        public override ItemStatus Run()
        {
            Stopwatch duration  = new Stopwatch();
            duration.Start();
            try
            {                
                if (Items.Count == 0)
                    Status = ItemStatus.Created;

                Items = Items.OrderBy(step => step.Order).ToList();

                foreach (Item item in Items)
                {
                    if (item.Type != ItemType.MultiResultTest)
                    {
                        Status = item.Run();
                    } 
                    else
                    {
                        // TODO MultiTread test run
                    }

                    if (Status.Equals(ItemStatus.Fail))
                        break;
                    Status = ItemStatus.Success;
                }
            }
            catch
            {
                Status = ItemStatus.Fail;
            }

            duration.Stop();
            Duration = duration.ElapsedMilliseconds;
            return Status;            
        }

        
    }
}
