using System;

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

        public virtual string Edit(string addedUrl, string body)
        {
            return new NotImplementedException().Message;
        }

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
