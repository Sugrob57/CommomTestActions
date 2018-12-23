using NUnit.Framework;
using CommonTestActions.Test;
using System;

namespace NUnitTests
{
    [TestFixture]
    public class TestTests
    {
        static Test LocalTest;
        static Test NetTest;
        static object[] LocalDataItems = Data.LocalTestData;
        static object[] NetDataItems = Data.NetTestData;

        [SetUp]
        public void Setup()
        {
            LocalTest = new Test("Sample local Test From NUnit");
            NetTest = new Test("Sample net Test From NUnit");
        }

        [Test, TestCaseSource("LocalDataItems")]
        public void LocalAddStep(ProviderType provider, ActionType action, string source, string query, string body = null)
        {
            LocalTest.AddStep(provider, action, source, query);
        }

        [Test, TestCaseSource("NetDataItems")]
        public void NetAddStep(ProviderType provider, ActionType action, string source, string query, string body = null)
        {
            NetTest.AddStep(provider, action, source, query);
        }

        //[TestCase(LocalTest)]
        //[TestCase(NetTest)]
        //public void RunTest(Test test)
        //{
        //    Test _test = test;
        //    try
        //    {
        //        ItemStatus _st = LocalTest.Run();
        //        Assert.IsNotNull(_st);
        //        //Assert.That(value, Is.EqualTo("string456").IgnoreCase);
        //    }
        //    catch (Exception e)
        //    {
        //        Assert.Fail(e.Message);
        //    }
        //}
    }
}
