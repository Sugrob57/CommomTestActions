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
        public string BaseUrl { get; set; } 
        public Dictionary<ParameterType,Object> Parameters { get; set; }
        public string Response { get; set; }

        public Step(ActionType action, string baseUrl, string resource = null) : base(action)
        {
            base.Name = "NonameStep";
            base.Type = ItemType.Step;
            BaseUrl = baseUrl;

            Parameters = new Dictionary<ParameterType, Object>();
            Parameters.Add(ParameterType.AddedUrl,resource);
        }


        public override ItemStatus Run()
        {
            Stopwatch duration = new Stopwatch();

            try
            {
                switch (Provider)
                {
                    case ProviderType.Rest:
                        duration.Start();
                        RestProvider _provider = new RestProvider();
                        _provider.ConectionString = BaseUrl;
                        //string _addedUrl = Parameters[ParameterType.AddedUrl].ToString();

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
                                duration.Stop();
                                Duration = duration.ElapsedMilliseconds;
                                break;
                            case ActionType.Create:
                                Response = _provider.Create(_addedUrl, _body).ToString();
                                Status = Response.Length == 0 ? ItemStatus.Fail : ItemStatus.Success;
                                duration.Stop();
                                Duration = duration.ElapsedMilliseconds;
                                break;
                            default:
                                Status = ItemStatus.Fail;
                                throw new InvalidOperationException();
                        }
                        break;
                    default:
                        Status = ItemStatus.Fail;
                        throw new NotImplementedException();
                }
            }
            catch (Exception e)
            {
                Status = ItemStatus.Fail;
            }

            return Status;
        }
    }
}
