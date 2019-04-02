using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTestActions.Test
{
    public enum ItemType
    {
        Step,
        Test,
        Pack,
        MultiResultTest
    }

    public enum TreadingType
    {
        Line,
        Delay,
        Parallel
    }


    public enum ItemStatus
    {
        Created,
        ReadyToRun,
        InProgress,
        Success,
        Fail
    }

    public enum ProviderType
    {
        Rest,
        Sql,
        NetTcp,
        Mq,
        Soap,
        Com
    }

    public enum ActionType
    {
        Create,
        Read,     
        Update,
        Delete,
        ExecuteValue,
        ExecuteList
    }

    public enum ParameterType
    {
        AddedUrl,
        Body,
        File,
        All,
        ReplacementValue,
        ReplacementParam
    }
}
