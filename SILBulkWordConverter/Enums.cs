using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SILConvertersWordML
{
    public enum ExecutionMode
    {
        Console,
        Web
    }

    public enum ConversionMode
    {
        BasicUserMode,
        ExpertUserMode
    }

    public enum ConverterType
    {
        Unknown,
        CP,
        TEC
    }

    public enum ResultType
    {
       Completed,
       Failed
    }

    public enum MessageType
    {
        UserMessage,    // Can be shown to the user
        UserErrorMessage,
        SystemMessage,  // Technical Message that can be logged for technical review
        SystemErrorMessage,
        ControlMessage  // For the request/response from the user
    }

    public enum ConversionElements
    {
        FontsAndStyles,
        FontsAlone,
        StylesAlone
    }

    public enum MessageLevel
    {
        Normal,
        Critical
    }

    public enum StepName
    {
        UploadSourceFiles,
        InitializeAndBasicChecks,
        LoadInputDocuments,
        ChooseConverters,
        ConvertAndSaveTargetFiles
    }

    public enum StepStatus
    {
        Started,
        Completed,
        WaitForUsersInput,
        LandedInError
    }
}
