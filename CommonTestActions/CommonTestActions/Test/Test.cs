using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CommonTestActions.Test
{
    public class Test : Item
    {
        public List<Item> Steps { get; set; }
        public Dictionary<string,string> StepResponses { get; set; }

        public Test(string name) : base()
        {
            if (name.Length > 0)
                base.Name = name;
            //TODO check name for unique
            else
                throw new ArgumentNullException();
            
            base.Type = ItemType.Test;

            Steps = new List<Item>();
            StepResponses = new Dictionary<string, string>();
        }



        public override ItemStatus Run()
        {
            return ItemStatus.Fail;
        }
        /*
            Stopwatch duration = new Stopwatch();
            duration.Start();
            try
            {


                switch (Provider)
                {
                    case ProviderType.Rest:
                        //RestProviderRun();
                        break;
                    default:
                        Status = ItemStatus.Fail;
                        throw new NotImplementedException();
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
        */

        public ItemStatus AddStep(ProviderType provider, ActionType action, string source, string query = null)
        {
            ItemStatus status = ItemStatus.ReadyToRun;
            try
            {
                Step _step = new Step(provider, action, source, query);
                _step.Order = Steps.Count + 1;
                _step.Name = String.Format("{0}_{1}_{2}_step", provider, action,_step.Order);
                Steps.Add(_step);
                status = ItemStatus.Success;
            }
            catch
            {
                status = ItemStatus.Fail;
            }
            return status;
        }
 
    }
}
