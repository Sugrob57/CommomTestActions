using NUnit.Framework;
using CommonTestActions.Providers;
using System;

namespace Tests
{
    [TestFixture]
    public class RestProviderTests
    {
        /// <summary>
        /// http://dotnetpattern.com/nunit-assert-examples 
        /// </summary>
        RestProvider provider;
        string Body;

        [SetUp]
        public void Setup()
        {
            provider = new RestProvider();
            Body = "{ \"Title\": \"string123\", \"enabled\": false}";
        }

        [TestCase("http://jsonplaceholder.typicode.com", "/todos")]
        [TestCase("http://localhost:56006", "/api/Topic")]
        public void ReadRequest(string source, string addedUrl)
        {
            try
            {
                provider.ConectionString = source;

                string response = provider.Read(addedUrl).ToString();
                Assert.IsNotNull(response);
                Assert.That(response, Does.Contain("{").IgnoreCase);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }         
        }

        [TestCase("http://jsonplaceholder.typicode.com", "/todos/2")]
        [TestCase("http://localhost:56006", "/api/Topic/2")]
        public void ReadByIdRequest(string source, string addedUrl)
        {
            try
            {
                provider.ConectionString = source;

                string response = provider.Read(addedUrl).ToString();
                Assert.IsNotNull(response);
                Assert.That(response, Does.Contain("{").IgnoreCase);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestCase("http://jsonplaceholder.typicode.com", "/todos")]
        [TestCase("http://localhost:56006", "/api/Topic")]
        public void CreateRequest(string source, string addedUrl)
        {
            try
            {
                provider.ConectionString = source;
                string _body = Body;

                string response = provider.Create(addedUrl, _body).ToString();
                Assert.IsNotNull(response);
                Assert.That(response, Does.Contain("{").IgnoreCase);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestCase("http://jsonplaceholder.typicode.com", "/todos/2")]
        [TestCase("http://localhost:56006", "/api/Topic/2")]
        public void UpdateRequest(string source, string addedUrl)
        {
            try
            {
                provider.ConectionString = source;
                string _body = Body;

                string response = provider.Update(addedUrl, _body).ToString();
                Assert.IsNotNull(response);
                Assert.That(response, Does.Contain("{").IgnoreCase);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestCase("http://jsonplaceholder.typicode.com", "/todos/2")]
        [TestCase("http://localhost:56006", "/api/Topic/2")]
        public void DeleteRequest(string source, string addedUrl)
        {
            try
            {
                provider.ConectionString = source;
                //string _body = Body;

                string response = provider.Delete(addedUrl).ToString();
                Assert.IsNotNull(response);
                Assert.That(response, Does.Contain("OK").IgnoreCase);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void ExecuteValue()
        {
            try
            {
                string response = "[{ \"Title\": \"string123\", \"enabled\": false},{ \"Title\": \"string456\", \"enabled\": false}]";
                string value = provider.ExecuteValue(response, "request.[1].Title");
                
                Assert.IsNotNull(value);
                Assert.That(value, Is.EqualTo("string456").IgnoreCase);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
        
    }
}