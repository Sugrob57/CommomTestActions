using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CommonTestActions
{
    public abstract class Provider
    {
        public string ConectionString { get; set; }

        public virtual object Read(string query)
        {
            return new NotImplementedException().Message;
        }

        public virtual object Create(string query, string body)
        {
            return new NotImplementedException().Message;
        }

        public virtual string Update(string query, string body)
        {
            return new NotImplementedException().Message;
        }

        public virtual string Delete(string query)
        {
            return new NotImplementedException().Message;
        }

        public virtual string ExecuteValue(string response, string query)
        {
            return new NotImplementedException().Message;
        }
    }
}
