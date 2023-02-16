namespace Common.BasicHelper.Util;

public struct RegexStrings
{
    public const string GUID_Part_Parttern = @"([A-Z]|[a-z]|[0-9])";

    public const string LiteDB_Name_Limit = @"([A-Z]|[a-z]|[0-9])";

    public const string Version_Parse_STR = @"([0-9]*\.[0-9]*\.[0-9]*(\.[0-9]*)?)";
}
