using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.BasicHelper.Core.Shell;

public static class CommandsExecutor
{
    /// <summary>
    /// 获取命令执行输出
    /// </summary>
    /// <param name="command">命令</param>
    /// <param name="args">参数</param>
    /// <param name="findInPath">是否在 Path 中寻找</param>
    /// <param name="action">对启动信息的行动</param>
    /// <returns>命令执行输出</returns>
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
    /// 异步获取命令执行输出
    /// </summary>
    /// <param name="command">命令</param>
    /// <param name="args">参数</param>
    /// <param name="findInPath">是否在 Path 中寻找</param>
    /// <param name="action">针对启动信息的动作</param>
    /// <param name="token">取消口令</param>
    /// <returns>命令执行输出</returns>
    public static async Task<string> GetExecutionResultAsync
    (
        string command,
        string args,
        bool findInPath = false,
        Action<ProcessStartInfo>? action = null,
        CancellationToken? token = default
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

        //var sb = new StringBuilder();

        //void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        //    => sb.AppendLine(outLine.Data);

        //process.OutputDataReceived += new DataReceivedEventHandler(OutputHandler);
        //process.ErrorDataReceived += new DataReceivedEventHandler(OutputHandler);

        process.Start();

        var output = process.StandardOutput.ReadToEnd();

        await Task.Run(process.WaitForExit);

        return output;
    }

}
