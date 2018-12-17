using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonTestActions;
using CommonTestActions.Providers;
using CommonTestActions.Test;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            TestRunRestProvider();

            TestRunStep();

            Console.ReadKey();
        }

        private static void TestRunStep()
        {
            Console.WriteLine("---------- REST Step Read() -----------");
            Step step = new Step(ActionType.Read ,"http://localhost:56006", @"/api/Topic");
            Console.WriteLine(step.Run());
            Console.WriteLine();
            Console.WriteLine(step.Response);
            Console.WriteLine("----------------------");

            Console.WriteLine("--------- REST Step Create() -------------");
            step = new Step(ActionType.Create, "http://localhost:56006", @"/api/Topic");
            string _body = "{ \"Title\": \"string123\", \"enabled\": false}";
            step.Parameters.Add(
                ParameterType.Body,
                _body);

            Console.WriteLine(step.Run());
            Console.WriteLine();
            Console.WriteLine(step.Response);
            Console.WriteLine("----------------------");

        }


        private static void TestRunRestProvider ()
        {
            RestProvider provider = new RestProvider();
            provider.ConectionString = "http://localhost:56006";
            Console.WriteLine(JsonConvert.SerializeObject(provider.Read(@"/api/Topic")));

            Console.WriteLine("------------------------");
            string _body = "{ \"Title\": \"string123\", \"enabled\": false}";
            Console.WriteLine(provider.Create(@"/api/Topic", _body));

            Console.WriteLine("------------------------");
            string response = provider.Read(@"/api/Topic").ToString();
            Console.WriteLine(response);

            Console.WriteLine("------------------------");
            Console.WriteLine(provider.ExecuteValue(response, "request.[2].id"));
            Console.WriteLine(provider.ExecuteValue(response, "request.[1].title"));

            Console.WriteLine("------------------------");
            response = provider.Read(@"/api/Topic/2").ToString();
            Console.WriteLine(response);

            Console.WriteLine("------------------------");
            _body = "{ \"Title\": \"~~PARAM1~~\", \"enabled\": true}";
            Request _req = new Request(_body);
            _body = _req.ReplaceStaticVar("PARAM1", "NewTopicFromParam");
            response = provider.Create(@"/api/Topic", _body).ToString();
            Console.WriteLine(response);

            Console.WriteLine("------------------------");
            _body = "{ \"Title\": \"~~@PARAM1~~\", \"enabled\": true}";
            _req = new Request(_body);
            _body = _req.ReplaceDynamicVar("PARAM1", "DynamicParam");
            response = provider.Create(@"/api/Topic", _body).ToString();
            Console.WriteLine(response);

            Console.WriteLine("------------------------");
            int newItemId = Convert.ToInt32(provider.ExecuteValue(response, "request.id"))-2;

            Console.WriteLine("Try Edit Item {0}", newItemId);
            _body = "{ \"Title\": \"NNNNNNewTitle(EditedItem)\", \"enabled\": true}";
            Console.WriteLine(provider.Edit(@"/api/Topic/" + newItemId, _body));

            Console.WriteLine("------------------------");
            response = string.Empty;
            response = provider.Read(@"/api/Topic").ToString();
            Console.WriteLine(response);

            Console.WriteLine("------------------------");
            Console.WriteLine("------------------------");
            Console.WriteLine("------------------------");
        }
    }
}
