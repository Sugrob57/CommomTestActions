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

        [TestCase, TestCaseSource("NetDataItems")]
        [TestCase, TestCaseSource("LocalDataItems")]
        public void RunTest(ProviderType provider, ActionType action, string source, string query, string body = null)
        {
            try
            {
                Test _test = new Test("123");
                _test.AddStep(provider, action, source, query, body);
                
                ItemStatus _localIS = _test.Run();
                Assert.That(_localIS, Is.EqualTo(ItemStatus.Success));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestCase("http://localhost:56006/api/Topic", "request.[1].title", "topic2")]
        [TestCase("http://localhost:56006/api/Topic", "request.[2].enabled", "False")]
        public void ExecuteValueTest(string source, string query, string expectedValue = null)
        {
            try
            {
                Test _test = new Test("123");
                _test.AddStep(ProviderType.Rest, ActionType.Read, source);
                string firstStepName = String.Format("{0}_{1}_{2}_step", ProviderType.Rest, ActionType.Read, 1);
                _test.AddStep(ProviderType.Rest, ActionType.ExecuteValue, firstStepName, query);

                string secondStepName = String.Format("{0}_{1}_{2}_step", ProviderType.Rest, ActionType.ExecuteValue, 2);

                string secondStepResp = string.Empty;
                
                ItemStatus _localIS = _test.Run();
                _test.StepResponses.TryGetValue(secondStepName, out secondStepResp);
                Assert.That(_localIS, Is.EqualTo(ItemStatus.Success));
                Assert.That(secondStepResp, Is.EqualTo(expectedValue));
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
