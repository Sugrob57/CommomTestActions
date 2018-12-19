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
                Console.WriteLine("What test run?");
                Console.WriteLine("O - OwnRestProvider, N - NetRestProvider, S - Steps");
                Console.WriteLine("TO - OwnRestTests  , TN - NetRestTests");

                str = Console.ReadLine().ToUpper();
                switch (str)
                {
                    case "O":
                        RunRestProvider("http://localhost:56006", @"/api/Topic");
                        break;
                    case "N":
                        RunRestProvider("http://jsonplaceholder.typicode.com", @"/todos");
                        break;
                    case "S":
                        RunSteps();
                        break;
                    case "TO":
                        RunTests("http://localhost:56006", @"/api/Topic");
                        break;

                    case "TN":
                        RunTests("http://jsonplaceholder.typicode.com", @"/todos");
                        break;
                    default:
                        Console.WriteLine("Uncnown command");
                        break;
                }
                Console.WriteLine("==========================================");
            }

            //Console.ReadKey();
        }
        private static void RunTests(string source, string query)
        {
            Console.WriteLine("---------- Run Test (RestProvider) -----------");
            Test test = new Test("Sample test");
            Console.WriteLine("Curent test state");
            test.PrintResults();

            //----------------------------------------------------------------
            //------Add step--
            ProviderType pr = ProviderType.Rest; 
            ActionType ac = ActionType.Read;
            string src = source;
            string qr = query;
            Console.WriteLine("Added step: Provider: {0}, Action: {1}, Source: {2}, Query: {3}",
                pr, ac, src, qr);
            test.AddStep(pr, ac, src, qr);

            //------Add step--
            pr = ProviderType.Rest;
            ac = ActionType.Create;
            src = source;
            qr = query;
            string _body = "{ \"Title\": \"string123\", \"enabled\": false}"; 
            Console.WriteLine("Added step: Provider: {0}, Action: {1}, Source: {2}, Query: {3}",
                pr, ac, src, qr);
            test.AddStep(pr, ac, src, qr, _body);

            //------Add step--
            pr = ProviderType.Rest;
            ac = ActionType.Read;
            src = source;
            qr = query + "/2";
            Console.WriteLine("Added step: Provider: {0}, Action: {1}, Source: {2}, Query: {3}",
                pr, ac, src, qr);
            test.AddStep(pr, ac, src, qr);

            //------Add step--
            pr = ProviderType.Rest;
            ac = ActionType.Update;
            src = source;
            qr = query + "2";
            _body = "{ \"Title\": \"string456\", \"enabled\": false}";
            Console.WriteLine("Added step: Provider: {0}, Action: {1}, Source: {2}, Query: {3}",
                pr, ac, src, qr);
            test.AddStep(pr, ac, src, qr, _body);

            //------Add step--
            pr = ProviderType.Rest;
            ac = ActionType.Delete;
            src = source;
            qr = query + "/2";
            Console.WriteLine("Added step: Provider: {0}, Action: {1}, Source: {2}, Query: {3}",
                pr, ac, src, qr);
            test.AddStep(pr, ac, src, qr);

            //------Add step--
            pr = ProviderType.Rest;
            ac = ActionType.Read;
            src = source;
            qr = query;
            Console.WriteLine("Added step: Provider: {0}, Action: {1}, Source: {2}, Query: {3}",
                pr, ac, src, qr);
            test.AddStep(pr, ac, src, qr);

            //--------------------------------------------------------------------------------------------------
            Console.WriteLine();
            Console.WriteLine("============ Run test ============");
            test.Run();
            Console.WriteLine();
            test.PrintResults();
        }

        private static void RunSteps()
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

        private static void RunRestProvider(string url, string addedUrl)
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
