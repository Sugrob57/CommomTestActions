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
                        RestProviderRun();
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

        private void RestProviderRun()
        {
            RestProvider _provider = new RestProvider();
            _provider.ConectionString = Source;

            string _body = string.Empty;
            object obj = null;

            Parameters.TryGetValue(ParameterType.AddedUrl, out obj);
            string _addedUrl = obj?.ToString();

            obj = null;
            Parameters.TryGetValue(ParameterType.Body, out obj);
            _body = obj?.ToString();

            switch (Action)
            {
                case ActionType.Read:
                    Response = _provider.Read(_addedUrl).ToString();
                    Status = Response.Length == 0 ? ItemStatus.Fail : ItemStatus.Success;
                    break;
                case ActionType.Create:
                    Response = _provider.Create(_addedUrl, _body).ToString();
                    Status = Response.Length == 0 ? ItemStatus.Fail : ItemStatus.Success;
                    break;
                case ActionType.Update:
                    Response = _provider.Update(_addedUrl, _body).ToString();
                    Status = Response.Length == 0 ? ItemStatus.Fail : ItemStatus.Success;
                    break;
                case ActionType.Delete:
                    Response = _provider.Delete(_addedUrl).ToString();
                    Status = Response.Length == 0 ? ItemStatus.Fail : ItemStatus.Success;
                    break;
                case ActionType.ExecuteValue:
                    Response = _provider.ExecuteValue(_body, _addedUrl);
                    Status = Response.Length == 0 ? ItemStatus.Fail : ItemStatus.Success;
                    break;
                default:
                    Status = ItemStatus.Fail;
                    throw new InvalidOperationException();
            }
        }
    }
}
