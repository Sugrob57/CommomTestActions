using System;
using System.Collections.Generic;
using System.Text;
using CommonTestActions.Providers;
using CommonTestActions.Test;

namespace CommonTestActions
{
    public class MultiStep : Step
    {
        List<string> ResponceCollection;

        public MultiStep(ProviderType provider, ActionType action, string source, string query = null) 
            : base(provider, action, source, query)
        {
            ResponceCollection = new List<string>();
        }
    }
}
