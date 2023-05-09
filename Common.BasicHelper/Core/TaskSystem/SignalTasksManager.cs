using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.BasicHelper.Core.TaskSystem;

public class SignalTasksManager
{
    private readonly Dictionary<string, SignalTask> SignalTasks = new();

    /// <summary>
    /// 触发信号
    /// </summary>
    /// <param name="signal">信号名称</param>
    /// <returns>任务管理器本身</returns>
    public SignalTasksManager RaiseSignal(string signal)
    {
        if (SignalTasks.TryGetValue(signal, out var signalTask))
        {
            signalTask.ExecuteAll();

            if (!signalTask.SignalReusable)
                SignalTasks.Remove(signal);
        }

        return this;
    }

    /// <summary>
    /// 信号发生时运行任务
    /// </summary>
    /// <param name="signal">信号</param>
    /// <param name="action">任务</param>
    /// <returns>任务管理器本身</returns>
    public SignalTasksManager SignalRun(string signal, Action action, bool reusable = false)
    {
        if (SignalTasks.TryGetValue(signal, out var signalTask))
            signalTask.AddAction(action);
        else
            SignalTasks.Add(
                signal,
                new SignalTask()
                    .ReuseSignal(reusable)
                    .AddAction(action)
            );

        return this;
    }

    /// <summary>
    /// 清除所有信号响应任务
    /// </summary>
    /// <returns>任务管理器本身</returns>
    public SignalTasksManager Clear()
    {
        SignalTasks.Clear();

        return this;
    }
}
