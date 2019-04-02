using CommonTestActions.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTestActions.Test
{
    public class ResponseCollection
    {
        public List<string> Values;
        public string StepName;
        public string ReplacementValue;
        public ParameterType ReplacementParam;

        public Dictionary<string, Step> MakeStepList(Step step)
        {
            Dictionary<string, Step> steps = new Dictionary<string, Step>();

            if (Values.Count > 0)                
                foreach (string value in Values)
                {
                    Step _step = new Step(step.Provider, step.Action, step.Source);
                    if ((ReplacementParam == ParameterType.AddedUrl)
                        || (ReplacementParam == ParameterType.All))
                    {              
                        string queryS = string.Empty;
                        object queryO;
                        if (step.Parameters.TryGetValue(ParameterType.AddedUrl, out queryO))
                        {

                            queryS = queryO.ToString();
                            string newQuery = Actions.ReplaceValues(queryS, ReplacementValue, value);
                            _step.Parameters.Add(ParameterType.AddedUrl, newQuery);
                        }
                    }

                    if ((ReplacementParam == ParameterType.Body)
                        || (ReplacementParam == ParameterType.All))
                    {
                        string bodyS = string.Empty;
                        object bodyO;
                        if (step.Parameters.TryGetValue(ParameterType.Body, out bodyO))
                        {

                            bodyS = bodyO.ToString();
                            string newBody = Actions.ReplaceValues(bodyS, ReplacementValue, value);
                            _step.Parameters.Add(ParameterType.Body, newBody);
                        }
                    }
                    
                    //ELSE ERROR
                    if ((ReplacementParam != ParameterType.Body) 
                        && (ReplacementParam != ParameterType.AddedUrl)
                        && (ReplacementParam != ParameterType.All)) 
                    {
                        throw new NotImplementedException();
                    }

                    steps.Add(value, step);
                }

            return steps;
        }
    }
}
