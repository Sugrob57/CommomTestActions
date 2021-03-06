﻿using CommonTestActions.Test;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CommonTestActions.Providers
{
    public class Actions
    {
        public static Step RestAction(string source, Dictionary<ParameterType, Object> parameters, ActionType action)
        {
            RestProvider _provider = new RestProvider();
            _provider.ConectionString = source;
            Step _step = new Step(ProviderType.Rest, action, source);
            _step.Parameters = parameters;

            string _body = string.Empty;
            object obj = null;

            parameters.TryGetValue(ParameterType.AddedUrl, out obj);
            string _addedUrl = obj?.ToString();

            obj = null;
            parameters.TryGetValue(ParameterType.Body, out obj);
            _body = obj?.ToString();

            switch (action)
            {
                case ActionType.Read:
                    _step.Response = _provider.Read(_addedUrl).ToString();
                    _step.Status = _step.Response.Length == 0 ? ItemStatus.Fail : ItemStatus.Success;
                    break;
                case ActionType.Create:
                    _step.Response = _provider.Create(_addedUrl, _body).ToString();
                    _step.Status = _step.Response.Length == 0 ? ItemStatus.Fail : ItemStatus.Success;
                    break;
                case ActionType.Update:
                    _step.Response = _provider.Update(_addedUrl, _body).ToString();
                    _step.Status = _step.Response.Length == 0 ? ItemStatus.Fail : ItemStatus.Success;
                    break;
                case ActionType.Delete:
                    _step.Response = _provider.Delete(_addedUrl).ToString();
                    _step.Status = _step.Response.Length == 0 ? ItemStatus.Fail : ItemStatus.Success;
                    break;
                case ActionType.ExecuteValue:
                    _step.Response = _provider.ExecuteValue(_body, _addedUrl);
                    _step.Status = _step.Response.Length == 0 ? ItemStatus.Fail : ItemStatus.Success;
                    break;
                case ActionType.ExecuteList:
                    _step.ResponseCollection = _provider.ExecuteList(_body, _addedUrl);
                    _step.Status = _step.ResponseCollection.Count == 0 ? ItemStatus.Fail : ItemStatus.Success;
                    break;
                default:
                    _step.Status = ItemStatus.Fail;
                    throw new InvalidOperationException();
            }

            return _step;
        }

        public static string ReplaceValues(string text, string oldValue, string newValue)
        {
            Regex regex = new Regex(String.Format("~~{0}~~", oldValue), RegexOptions.IgnoreCase);
            text = regex.Replace(text, newValue);
            return text;
        }
    }
}
