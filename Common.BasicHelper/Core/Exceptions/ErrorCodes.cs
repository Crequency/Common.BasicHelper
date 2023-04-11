namespace Common.BasicHelper.Core.Exceptions;

public enum ErrorCodes
{
    Undefined,
    CB0017,
    CB0027,
    CB0033,
    CB0034,
}

public class ErrorCodesHelper
{
    public static string GetExceptionMessage
    (
        ErrorCodes ec,
        string? attachment = null,
        string? functionName = null,
        string? parameterName = null,
        string? inputFormatRequirements = null,
        string? inputLengthRequirements = null
    )
        => ec switch
        {
            ErrorCodes.CB0017 => $"Only use `{functionName}` function for same types. {attachment}",
            ErrorCodes.CB0027 => $"Parameter `{parameterName}` not found. {attachment}",
            ErrorCodes.CB0033 => $"Error input format. {inputFormatRequirements} {attachment}",
            ErrorCodes.CB0034 => $"Error input length. {inputLengthRequirements} {attachment}",
            _ => $"Message for {ec} not found. {attachment}",
        };
}

public static class ErrorCodesExtensions
{
    public static string BuildMessage
    (
        this ErrorCodes ec,
        string? attachment = null,
        string? functionName = null,
        string? parameterName = null,
        string? inputFormatRequirements = null,
        string? inputLengthRequirements = null
    )
        => ErrorCodesHelper.GetExceptionMessage
        (
            ec,
            attachment,
            functionName,
            parameterName,
            inputFormatRequirements,
            inputLengthRequirements
        );
}
