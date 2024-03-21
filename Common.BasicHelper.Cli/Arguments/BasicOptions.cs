using CommandLine;

namespace Common.BasicHelper.Cli.Arguments;

public class BasicOptions
{

    [Option("verbose", HelpText = "Display verbose output.")]
    public bool Verbose { get; set; }

    [Option("dry-run", HelpText = "Dry run the command.")]
    public bool DryRun { get; set; }
}
