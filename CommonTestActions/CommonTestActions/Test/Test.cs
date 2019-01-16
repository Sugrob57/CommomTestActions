using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;


namespace CommonTestActions.Test
{
    public class Test : Item
    {
        public List<Step> Steps { get; set; }
        public Dictionary<string,string> StepResponses { get; set; }

        public Test(string name) : base()
        {
            if (name.Length > 0)
                base.Name = name;
            //TODO check name for unique
            else
                throw new ArgumentNullException();
            
            base.Type = ItemType.Test;

            Steps = new List<Step>();
            StepResponses = new Dictionary<string, string>();
        }

        public override ItemStatus Run()
        {
            Stopwatch duration = new Stopwatch();
            duration.Start();
            try
            {
                SortSteps();
                if (Steps.Count == 0)
                    Status = ItemStatus.Created;

                foreach (Step step in Steps)
                {
                    if (step.Action == ActionType.ExecuteValue)
                        if (StepResponses.TryGetValue(step.Source, out string resp))
                        {
                            step.Parameters.Add(ParameterType.Body, resp);
                        }
                        else
                        {
                            Status = ItemStatus.Fail;
                            break;
                        }
                    
                    Status = step.Run();
                    step.PrintResults();
                    StepResponses.Add(step.Name, step.Response);
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
        
        private void SortSteps()
        {
            Steps = Steps.OrderBy(step => step.Order).ToList();
        }

        public ItemStatus AddStep(ProviderType provider, ActionType action, string source, string query = null, string body = null)
        {
            ItemStatus status = ItemStatus.ReadyToRun;
            try
            {
                Step _step = new Step(provider, action, source, query);
                _step.Order = Steps.Count+1;
                _step.Name = String.Format("{0}_{1}_{2}_step", provider, action, _step.Order);
                if (body != null)
                    _step.Parameters.Add(ParameterType.Body, body);
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
