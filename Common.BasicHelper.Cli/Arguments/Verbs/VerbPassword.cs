using CommandLine;
using Common.BasicHelper.Utils;

namespace Common.BasicHelper.Cli.Arguments.Verbs;

[Verb("password", aliases: ["passwd", "pwd"], HelpText = "Password related utils.")]
public class VerbPassword : BasicOptions
{
    [Option('g', "generate", Default = true, HelpText = "Generate a password.")]
    public bool Generate { get; set; }

    [Option('l', "length", Default = 12, HelpText = "Length of the password.")]
    public int Length { get; set; }

    [Option('r', "length-range", HelpText = "Length range of the password, like `3,5`.")]
    public string? LengthRange { get; set; }

    [Option('u', "ignore-uppercase", HelpText = "Ignore uppercase letters.")]
    public bool IgnoreUppercase { get; set; }

    [Option('e', "ignore-lowercase", HelpText = "Ignore lowercase letters.")]
    public bool IgnoreLowercase { get; set; }

    [Option('n', "ignore-numbers", HelpText = "Ignore numbers.")]
    public bool IgnoreNumbers { get; set; }

    [Option('s', "ignore-symbols", HelpText = "Ignore symbols.")]
    public bool IgnoreSymbols { get; set; }

    [Option('U', "supported-uppercase", Default = "ABCDEFGHIJKLMNOPQRSTUVWXYZ", HelpText = "Supported uppercase letters.")]
    public string? SupportedUppercase { get; set; }

    [Option('E', "supported-lowercase", Default = "abcdefghijklmnopqrstuvwxyz", HelpText = "Supported lowercase letters.")]
    public string? SupportedLowercase { get; set; }

    [Option('N', "supported-numbers", Default = "0123456789", HelpText = "Supported numbers.")]
    public string? SupportedNumbers { get; set; }

    [Option('S', "supported-symbols", Default = "!@#$%^&*()_+-=[]{};':,./<>?", HelpText = "Supported symbols.")]
    public string? SupportedSymbols { get; set; }
}

public static class PasswordExtensions
{
    public static VerbPassword Execute(this VerbPassword pwdc)
    {
        int lengthRangeStart = 0;
        int lengthRangeEnd = 0;

        var lengthRangeProvided = pwdc.LengthRange is not null;

        if (pwdc.LengthRange is not null)
        {
            var splited = pwdc.LengthRange.Split(',');

            if (splited.Length != 2)
                throw new ArgumentException("Length range should be like `3,5`.");

            lengthRangeProvided = lengthRangeProvided && int.TryParse(splited[0], out lengthRangeStart);
            lengthRangeProvided = lengthRangeProvided && int.TryParse(splited[1], out lengthRangeEnd);
        }

        var pwd = Password.GeneratePassword(
            pwdc.Length,
            lengthRangeProvided ? lengthRangeStart : null,
            lengthRangeProvided ? lengthRangeEnd : null,
            pwdc.IgnoreUppercase == false,
            pwdc.IgnoreLowercase == false,
            pwdc.IgnoreNumbers == false,
            pwdc.IgnoreSymbols == false,
            pwdc.SupportedUppercase!,
            pwdc.SupportedLowercase!,
            pwdc.SupportedNumbers!,
            pwdc.SupportedSymbols!
        );

        Console.WriteLine(pwd);

        return pwdc;
    }
}
