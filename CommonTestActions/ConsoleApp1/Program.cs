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
            string str = string.Empty;
            while (!str.Equals("E"))
            {
                Console.WriteLine("What test run? (O - OwnRestProvider, N - NetRestProvider, S - Steps)");
                str = Console.ReadLine().ToUpper();
                switch (str)
                {
                    case "O":
                        TestRestProvider("http://localhost:56006", @"/api/Topic");
                        break;
                    case "N":
                        TestRestProvider("http://jsonplaceholder.typicode.com", @"/todos");
                        break;
                    case "S":
                        TestRunStep();
                        break;
                    default:
                        Console.WriteLine("Uncnown command");
                        break;
                }
                Console.WriteLine("==========================================");
            }
            //TestRunRestProvider();
            //TestRunRestProviderUseJsonServer();
            //TestRunStep();

            //Console.ReadKey();
        }

        private static void TestRunStep()
        {
            Console.WriteLine("---------- REST Step Read() -----------");
            Step step = new Step(ProviderType.Rest,ActionType.Read ,"http://localhost:56006", @"/api/Topic");
            Console.WriteLine(step.Run());
            Console.WriteLine();
            Console.WriteLine(step.Response);
            Console.WriteLine("----------------------");

            Console.WriteLine("--------- REST Step Create() -------------");
            step = new Step(ProviderType.Rest,ActionType.Create, "http://localhost:56006", @"/api/Topic");
            string _body = "{ \"Title\": \"string123\", \"enabled\": false}";
            step.Parameters.Add(
                ParameterType.Body,
                _body);

            Console.WriteLine(step.Run());
            Console.WriteLine();
            Console.WriteLine(step.Response);
            Console.WriteLine("----------------------");

        }

        private static void TestRestProvider(string url, string addedUrl)
        {
            RestProvider provider = new RestProvider();
            provider.ConectionString = url; 

            Console.WriteLine("--------GET Request----------------");
            string response = provider.Read(addedUrl).ToString();
            Console.WriteLine(JsonConvert.SerializeObject(response));

            Console.WriteLine();
            Console.WriteLine("---------POST request---------------");
            string _body = "{ \"Title\": \"string123\", \"enabled\": false}";
            Console.WriteLine(provider.Create(addedUrl, _body));

            Console.WriteLine();
            Console.WriteLine("--------GET Request----------------");
            response = provider.Read(addedUrl).ToString();
            Console.WriteLine(response);

            Console.WriteLine();
            Console.WriteLine("---------DELETE request---------------");
            int newItemId = 3;//Convert.ToInt32(provider.ExecuteValue(response, "request.id"));
            Console.WriteLine("Try Delete Item {0}", newItemId);
            Console.WriteLine(provider.Delete(addedUrl + @"/"+ newItemId)); 

            Console.WriteLine();
            Console.WriteLine("--------GET request----------------");
            response = string.Empty;
            response = provider.Read(addedUrl).ToString();
            Console.WriteLine(response);

            newItemId = 2;
            Console.WriteLine();
            Console.WriteLine("---------GET request by Id---------------");
            response = provider.Read(addedUrl +@"/" + newItemId).ToString();
            Console.WriteLine(response);

            Console.WriteLine();
            Console.WriteLine("---------PUT request---------------");
            Console.WriteLine("Try Edit Item {0}", newItemId);
            _body = "{ \"Title\": \"NNNNNNewTitle(EditedItem)\", \"enabled\": true}";
            Console.WriteLine(provider.Update(addedUrl + @"/" + newItemId, _body));

            //Console.WriteLine("------------------------");
            //response = string.Empty;
            //response = provider.Read(@"/api/Topic").ToString();
            //Console.WriteLine(response);

            //Console.WriteLine("------------------------");
            //Console.WriteLine(provider.ExecuteValue(response, "request.[2].id"));
            //Console.WriteLine(provider.ExecuteValue(response, "request.[1].title"));

            //Console.WriteLine("------------------------");
            //response = provider.Read(@"/api/Topic/2").ToString();
            //Console.WriteLine(response);

            //Console.WriteLine("------------------------");
            //_body = "{ \"Title\": \"~~PARAM1~~\", \"enabled\": true}";
            //Request _req = new Request(_body);
            //_body = _req.ReplaceStaticVar("PARAM1", "NewTopicFromParam");
            //response = provider.Create(@"/api/Topic", _body).ToString();
            //Console.WriteLine(response);

            //Console.WriteLine("------------------------");
            //_body = "{ \"Title\": \"~~@PARAM1~~\", \"enabled\": true}";
            //_req = new Request(_body);
            //_body = _req.ReplaceDynamicVar("PARAM1", "DynamicParam");
            //response = provider.Create(@"/api/Topic", _body).ToString();
            //Console.WriteLine(response);

            Console.WriteLine("------------------------");
            Console.WriteLine("------------------------");
            Console.WriteLine("------------------------");
        }
    }
}
