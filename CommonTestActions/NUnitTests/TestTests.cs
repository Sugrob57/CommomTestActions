using NUnit.Framework;
using CommonTestActions.Test;
using System;

namespace NUnitTests
{
    [TestFixture]
    public class TestTests
    {
        Test LocalTest;
        Test NetTest;
        static object[] LocalDataItems = Data.LocalTestData;
        static object[] NetDataItems = Data.NetTestData;

        [SetUp]
        public void Setup()
        {
            LocalTest = new Test("Sample local Test From NUnit");
            NetTest = new Test("Sample net Test From NUnit");
        }

        [Test, TestCaseSource("LocalDataItems")]
        [Order(1)]
        public void LocalAddStep(ProviderType provider, ActionType action, string source, string query, string body = null)
        {
            Test _test = LocalTest;
            try
            {
                int _count = _test.Steps.Count;
                _test.AddStep(provider, action, source, query);
                Assert.That(_test.Steps.Count, Is.EqualTo(_count + 1));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            LocalTest = _test;
        }

        [Test, TestCaseSource("NetDataItems")]
        [Order(2)]
        public void NetAddStep(ProviderType provider, ActionType action, string source, string query, string body = null)
        {
            Test _test = NetTest;
            try
            {
                int _count = _test.Steps.Count;
                _test.AddStep(provider, action, source, query);
                Assert.That(_test.Steps.Count, Is.EqualTo(_count+1));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            NetTest = _test;
        }

        //[TestCase(LocalTest)]
        //[TestCase(NetTest)]
        [TestCase]
        [Order(3)]
        public void RunTest()
        {
            try
            {
                Test _test = new Test("123");
                _test.AddStep(ProviderType.Rest, ActionType.Read, Data.LocalSource, Data.LocalAddedUrl);
                _test.AddStep(ProviderType.Rest, ActionType.Create, Data.LocalSource, Data.LocalAddedUrl, Data.Body);
                
                ItemStatus _localIS = _test.Run();
                Assert.That(_localIS, Is.EqualTo(ItemStatus.Success));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
