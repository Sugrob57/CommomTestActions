using NUnit.Framework;
using CommonTestActions.Test;
using System;

namespace NUnitTests
{
    class Data
    {
        static string NetSource = "http://jsonplaceholder.typicode.com";
        static string LocalSource = "http://localhost:56006";
        static string NetAddedUrl = "/todos";
        static string LocalAddedUrl = "/api/Topic";
        static string Body = "{ \"Title\": \"string123\", \"enabled\": false}";


        public static object[] LocalTestData =
        {
            new object[] { ProviderType.Rest, ActionType.Read, LocalSource, LocalAddedUrl, null },
            new object[] { ProviderType.Rest, ActionType.Read, LocalSource, LocalAddedUrl + "/2", null },
            new object[] { ProviderType.Rest, ActionType.Create, LocalSource, LocalAddedUrl, Body },
            new object[] { ProviderType.Rest, ActionType.Update, LocalSource, LocalAddedUrl + "/2", Body },
            new object[] { ProviderType.Rest, ActionType.Delete, LocalSource, LocalAddedUrl + "/2", null }
        };

        public static object[] NetTestData =
        {
            new object[] { ProviderType.Rest, ActionType.Read, NetSource, NetAddedUrl, null },
            new object[] { ProviderType.Rest, ActionType.Read, NetSource, NetAddedUrl + "/2", null },
            new object[] { ProviderType.Rest, ActionType.Create, NetSource, NetAddedUrl, Body },
            new object[] { ProviderType.Rest, ActionType.Update, NetSource, NetAddedUrl + "/2", Body },
            new object[] { ProviderType.Rest, ActionType.Delete, NetSource, NetAddedUrl + "/2", null }
        };

    }
}
