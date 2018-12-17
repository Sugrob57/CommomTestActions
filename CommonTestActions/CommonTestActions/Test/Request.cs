using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonTestActions
{
    public class Request
    {
        public string RequestText { get; set; }

        public Request (string request)
        {
            RequestText = request;
        }

        public string ReplaceStaticVar (string var, string resultText)
        {
            string patern = String.Format("~~{0}~~", var);//inputText;//@"~~@(\w*)~~";
            return ReplaceStaticText(patern, resultText);
        }

        public string ReplaceDynamicVar(string var, string resultText)
        {
            string patern = String.Format("~~@{0}~~", var);
            return ReplaceStaticText(patern, resultText);
        }

        public string ReplaceStaticText (string inputText, string resultText)
        {
            string patern = inputText;
            string target = resultText;
            Regex regex = new Regex(patern);
            string result = regex.Replace(RequestText, target);

            RequestText = result;
            return result;
        }
    }
}
