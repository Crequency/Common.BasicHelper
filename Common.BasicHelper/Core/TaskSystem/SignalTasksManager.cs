using System;
using System.Collections.Generic;

namespace Common.BasicHelper.Core.TaskSystem;

public class SignalTasksManager
{
    private readonly Dictionary<string, SignalTask> SignalTasks = [];

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

    public SignalTasksManager Clear()
    {
        SignalTasks.Clear();

        return this;
    }
}
