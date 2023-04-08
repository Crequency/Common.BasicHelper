using System;
using System.Linq;
using System.Text;

namespace Common.BasicHelper.Utils;

public class Password
{
    public const string AllUppercases = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public const string AllLowercases = "abcdefghijklmnopqrstuvwxyz";

    public const string AllNumbers = "0123456789";

    public const string AllSymbols = @"!@#$%^&*()_+-=[]{};':,./<>?";

    /// <summary>
    /// 生成密码
    /// </summary>
    /// <param name="length">确切长度</param>
    /// <param name="lengthRangeStart">长度范围始</param>
    /// <param name="lengthRangeEnd">长度范围终</param>
    /// <param name="includeUppercase">是否包含大写字母</param>
    /// <param name="includeLowercase">是否包含小写字母</param>
    /// <param name="includeNumbers">是否包含数字</param>
    /// <param name="includeSymbols">是否包含符号</param>
    /// <param name="supportedUppercases">支持的大写字母集</param>
    /// <param name="supportedLowercases">支持的小写字母集</param>
    /// <param name="supportedNumbers">支持的数字集</param>
    /// <param name="supportedSymbols">支持的符号集</param>
    /// <returns>生成的密码</returns>
    public static string GeneratePassword
    (
        int? length = null,
        int? lengthRangeStart = null,
        int? lengthRangeEnd = null,
        bool includeUppercase = true,
        bool includeLowercase = true,
        bool includeNumbers = true,
        bool includeSymbols = true,
        string supportedUppercases = AllUppercases,
        string supportedLowercases = AllLowercases,
        string supportedNumbers = AllNumbers,
        string supportedSymbols = AllSymbols
    )
    {
        #region Guard Blocks

        var actualLengthProvided = length is not null;

        var rangeLengthProvided = lengthRangeStart is not null && lengthRangeEnd is not null;

        var lengthProvided = actualLengthProvided || rangeLengthProvided;

        var noCharIncluded = true
            && !includeUppercase
            && !includeLowercase
            && !includeNumbers
            && !includeSymbols;

        var noSupportedChars = true
            && supportedUppercases.Length == 0
            && supportedLowercases.Length == 0
            && supportedNumbers.Length == 0
            && supportedSymbols.Length == 0;

        if (!lengthProvided) throw new ArgumentException("CB0027: No length provided.");

        if (noCharIncluded) throw new ArgumentException("CB0028: At least one char type included.");

        if (noSupportedChars) throw new ArgumentException("CB0029: No supported chars provided.");

        #endregion Guard Blocks

        var sb = new StringBuilder();

        var random = new Random();

        var chars = new string[4]
        {
            supportedUppercases,
            supportedLowercases,
            supportedNumbers,
            supportedSymbols
        };

        var generateLength = length
            ?? random.Next(lengthRangeStart ?? 0, lengthRangeEnd ?? 13);

        var selected = from item in Enumerable.Range(0, generateLength)
                       let selection = chars[random.Next(0, chars.Length)]
                       select selection[random.Next(0, selection.Length)];

        foreach (var item in selected)
            sb.Append(item);

        return sb.ToString();
    }
}
