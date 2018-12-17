using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CommonTestActions
{
    public abstract class Provider
    {
        public string ConectionString { get; set; }

        public virtual object Read(string addedUrl)
        {
            return new NotImplementedException().Message;
        }

        public virtual object Create(string addedUrl, string body)
        {
            return new NotImplementedException().Message;
        }

        public abstract string Edit(string addedUrl, string body);
        //{
        //    return new Task<HttpResponseMessage>();
        //}

        public virtual string Delete(string addedUrl)
        {
            return new NotImplementedException().Message;
        }

        public virtual string ExecuteValue(string response, string query)
        { 
            return  "123";
        }
    }
}
