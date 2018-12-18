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
        MultiResponsePack
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
        ExecuteValue
    }

    public enum ParameterType
    {
        AddedUrl,
        Body,
        File
    }
}
