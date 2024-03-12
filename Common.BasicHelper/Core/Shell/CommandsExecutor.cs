using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Common.BasicHelper.Core.Shell;

public static class CommandsExecutor
{
    /// <summary>
    /// Get standard output from command executed in system shell
    /// </summary>
    /// <param name="command">Command</param>
    /// <param name="args">Arguments</param>
    /// <param name="findInPath">Should find command file path from system variable "Path"</param>
    /// <param name="action">Further action for `ProcessStartInfo` processing</param>
    /// <returns>Standard output for command</returns>
    public static string GetExecutionResult
    (
        string command,
        string args,
        bool findInPath = false,
        Action<ProcessStartInfo>? action = null
    )
    {
        if (findInPath)
            command = EnvironmentHelper.GetFilePathInPaths(command);

        var psi = new ProcessStartInfo()
        {
            FileName = command,
            Arguments = args,
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = false,
            CreateNoWindow = true,
        };
        action?.Invoke(psi);

        var process = new Process
        {
            StartInfo = psi,
        };

        process.Start();

        var output = process.StandardOutput.ReadToEnd();

        process.WaitForExit();

        return output;
    }

    /// <summary>
    /// Get standard output from command executed in system shell asyncly
    /// </summary>
    /// <param name="command">Command</param>
    /// <param name="args">Arguments</param>
    /// <param name="findInPath">Should find command file path from system variable "Path"</param>
    /// <param name="action">Further action for `ProcessStartInfo` processing</param>
    /// <param name="token">Cancellation token</param>
    /// <returns>Standard output for command</returns>
    public static async Task<string> GetExecutionResultAsync
    (
        string command,
        string args,
        bool findInPath = false,
        Action<ProcessStartInfo>? action = null,
        CancellationToken? token = null
    )
    {
        if (findInPath)
            await Task.Run(() => command = EnvironmentHelper.GetFilePathInPaths(command));

        var psi = new ProcessStartInfo()
        {
            FileName = command,
            Arguments = args,
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = false,
            CreateNoWindow = true,
        };
        action?.Invoke(psi);

        var process = new Process
        {
            StartInfo = psi,
        };

        process.Start();

        var output = process.StandardOutput.ReadToEnd();

        await Task.Run(() =>
        {
            if (token is null) process.WaitForExit();

            while (!process.HasExited)
            {
                if (token is not null && token.Value.IsCancellationRequested)
                {
                    process.Kill();

                    break;
                }
            }
        });

        return output;
    }

}

public static class CommandsExecutorExtensions
{

    /// <summary>
    /// Regard this string as command and return standard output as it executed
    /// </summary>
    /// <param name="command">Command</param>
    /// <param name="args">Arguments</param>
    /// <param name="findInPath">Should find command file path from system variable "Path"</param>
    /// <param name="action">Further action for `ProcessStartInfo` processing</param>
    /// <returns>Standard output from this command</returns>
    public static string ExecuteAsCommand
    (
        this string command,
        string? args = null,
        bool findInPath = true,
        Action<ProcessStartInfo>? action = null
    )
        => CommandsExecutor.GetExecutionResult
        (
            command,
            args ?? "",
            findInPath,
            action
        );


    /// <summary>
    /// Regard this string as command and return standard output as it executed asyncly
    /// </summary>
    /// <param name="command">Command</param>
    /// <param name="args">Arguments</param>
    /// <param name="findInPath">Should find command file path from system variable "Path"</param>
    /// <param name="action">Further action for `ProcessStartInfo` processing</param>
    /// <param name="token">Cancellation token</param>
    /// <returns>Standard output from this command</returns>
    public static Task<string> ExecuteAsCommandAsync
    (
        this string command,
        string? args = null,
        bool findInPath = true,
        Action<ProcessStartInfo>? action = null,
        CancellationToken? token = default
    )
        => CommandsExecutor.GetExecutionResultAsync
        (
            command,
            args ?? "",
            findInPath,
            action,
            token
        );
}
