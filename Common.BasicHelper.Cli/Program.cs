using CommandLine;
using Common.BasicHelper.Cli.Arguments.Verbs;

Parser.Default.ParseArguments<VerbPassword, object>(args)
    .WithParsed<VerbPassword>(options => options.Execute())
    ;