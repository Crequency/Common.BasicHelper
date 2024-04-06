using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.BasicHelper.Core.Exceptions;
using Common.BasicHelper.Utils.Extensions;

namespace Common.BasicHelper.Utils;

public class Password
{
    public const string AllUppercases = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public const string AllLowercases = "abcdefghijklmnopqrstuvwxyz";

    public const string AllNumbers = "0123456789";

    public const string AllSymbols = @"!@#$%^&*()_+-=[]{};':,./<>?";

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

        if (!lengthProvided)
            ErrorCodes.CB0027
                .BuildMessage(
                    parameterName: "length | lengthRangeStart | lengthRangeEnd",
                    attachment: "At least one type of length provided."
                ).Throw<ArgumentException>();

        if (noCharIncluded)
            ErrorCodes.CB0027
                .BuildMessage(
                    parameterName: "includeUppercase | includeLowercase | includeNumbers | includeSymbols",
                    attachment: "At least one element type included."
                ).Throw<ArgumentException>();

        if (noSupportedChars)
            ErrorCodes.CB0027
                .BuildMessage(
                    parameterName: "" +
                        "supportedUppercases | " +
                        "supportedLowercases | " +
                        "supportedNumbers | " +
                        "supportedSymbols",
                    attachment: "No supported chars provided."
                ).Throw<ArgumentException>();

        #endregion Guard Blocks

        var sb = new StringBuilder();

        var random = new Random();

        var chars = new List<string>();

        if (includeUppercase)
            chars.Add(supportedUppercases);

        if (includeLowercase)
            chars.Add(supportedLowercases);

        if (includeNumbers)
            chars.Add(supportedNumbers);

        if (includeSymbols)
            chars.Add(supportedSymbols);

        var generateLength = length ?? random.Next(lengthRangeStart ?? 0, lengthRangeEnd ?? 13);

        var selected = from item in Enumerable.Range(0, generateLength)
                       let selection = chars[random.Next(0, chars.Count)]
                       select selection[random.Next(0, selection.Length)];

        foreach (var item in selected)
            sb.Append(item);

        return sb.ToString();
    }
}
