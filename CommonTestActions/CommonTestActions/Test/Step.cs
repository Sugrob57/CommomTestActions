using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using CommonTestActions.Providers;

namespace CommonTestActions.Test
{
    public class Step : Item
    {
        public ProviderType Provider { get; }
        public string Source { get; set; } 
        public Dictionary<ParameterType,Object> Parameters { get; set; }
        public string Response { get; set; }
        public ActionType Action { get; set; }

        public Step(ProviderType provider,ActionType action, string source, string query = null) : base()
        {
            base.Name = "NonameStep";
            base.Type = ItemType.Step;
            Source = source;
            Action = action;

            Parameters = new Dictionary<ParameterType, Object>();
            Parameters.Add(ParameterType.AddedUrl,query);
        }


        public override ItemStatus Run()
        {
            Stopwatch duration = new Stopwatch();
            duration.Start();
            try
            {  
                switch (Provider)
                {
                    case ProviderType.Rest:
                        Step step = RunActions.RunRestAction(Source,Parameters,Action);
                        Status = step.Status;
                        Response = step.Response;
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

        
    }
}
