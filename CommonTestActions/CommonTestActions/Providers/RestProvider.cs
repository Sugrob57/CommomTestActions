﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

//using 

namespace CommonTestActions.Providers
{
    public class RestProvider : Provider
    {
        public override object Create(string addedUrl, string body)
        {
            var client = new RestClient(base.ConectionString);
            var request = new RestRequest(addedUrl, Method.POST);
            var _body = ParseString2Json(body);
            request.AddParameter("application/json; charset=utf-8", _body, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            var queryResult = client.Execute(request);
            return queryResult.Content;
        }

        public override string Delete(string addedUrl)
        {
            //var item = new Item(){//body};
            //var client = new RestClient("http://192.168.0.1");
            //var request = new RestRequest("api/item/{id}", Method.DELETE);
            //request.AddParameter("id", idItem);

            //client.Execute(request)
            return base.Delete(addedUrl);
        }

        public override string Edit(string addedUrl, string body)
        {
            //return "yui";
            //return RestSharpEdit(addedUrl, body);
            return BaseEdit(addedUrl, body);

            //var client = new RestClient(base.ConectionString);
            //var request = new RestRequest(@"/api/Topic/4/", Method.PUT);

            //var _body = ParseString2Json(body);

            ////request.AddParameter("id", 4);
            //request.AddParameter("application/json; charset=utf-8", _body, ParameterType.RequestBody);
            ////request.AddJsonBody(_body);
            //request.RequestFormat = DataFormat.Json;

            //var queryResult = client.Put(request);

            ////queryResult = client.Delete(request);
            //return queryResult.Content;

            //-------------
            //var client = new RestClient(base.ConectionString);
            //var request = new RestRequest("/api/Topic/{id}", Method.PUT);
            //request.AddUrlSegment("id", "4");
            //var _body = ParseString2Json(body);
            //request.AddParameter("application/json", _body, ParameterType.RequestBody);
            //var queryResult = client.Execute(request);
            //return queryResult.Content;
        }

        private string RestSharpEdit(string addedUrl, string body)
        {
            var client = new RestClient(base.ConectionString);
            var request = new RestRequest(addedUrl, Method.PUT);

            var _body = ParseString2Json(body);

            request.AddParameter("application/json; charset=utf-8", _body, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            var queryResult = client.Execute(request);
            return queryResult.Content;
        }

        private string BaseEdit(string addedUrl, string body)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(base.ConectionString);

                var converter = new IsoDateTimeConverter();
                converter.DateTimeStyles = DateTimeStyles.AdjustToUniversal;
                var json = JsonConvert.SerializeObject(body, converter);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = httpClient.PutAsync(addedUrl, content).Result;

                return response.Content.ReadAsStringAsync().Result;
            }
        }

        public override object Read(string addedUrl)
        {
            var client = new RestClient(base.ConectionString);
            var request = new RestRequest(addedUrl, Method.GET);
            var queryResult = client.Execute(request);
            return queryResult.Content;
        }

        private static JObject ParseString2Json(string str)
        {
            JObject jobject = new JObject();
            jobject = JObject.Parse(str);

            return jobject;
        }

        public override string ExecuteValue(string response, string query)
        {
            JObject jobject = new JObject();
            string str = "{ 'request': " + response + " }";
            jobject = ParseString2Json(str);

            string value = jobject.SelectToken(query).ToString();
            return value;
            // cm http://www.newtonsoft.com/json/help/html/QueryingLINQtoJSON.htm
            // https://www.newtonsoft.com/json/help/html/SelectToken.htm
        }
    }
}
