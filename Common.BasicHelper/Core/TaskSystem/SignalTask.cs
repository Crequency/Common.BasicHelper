using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.BasicHelper.Utils.Extensions;

namespace Common.BasicHelper.Core.TaskSystem;

public class SignalTask
{
    public Queue<Action> Actions { get; } = new();

    public bool SignalReusable { get; set; } = false;

    public bool ActionsReusable { get; set; } = false;

    public SignalTask AddAction(Action action)
    {
        Actions.Enqueue(action);

        return this;
    }

    public SignalTask ReuseSignal(bool reusable = true)
    {
        SignalReusable = reusable;

        return this;
    }

    public SignalTask ReuseActions(bool reusable = true)
    {
        ActionsReusable = reusable;

        return this;
    }

    public SignalTask ExecuteAll(bool? reuseall = null)
    {
        Actions.ForEach(action => action(), reuseall ?? ActionsReusable);

        return this;
    }

    public async Task<SignalTask> ExecuteAllAsync(bool? reuseall = null, CancellationToken? token = null)
    {
        await Actions.ForEachAsync(
            action => action(),
            reuseall ?? ActionsReusable,
            token ?? default
        );

        return this;
    }
}

public static class SignalTaskExtensions
{
    public static SignalTask AddTo(this Action action, SignalTask task) => task.AddAction(action);

    public static SignalTask ExecuteAll(this SignalTask task, bool reuseAll = false)
        => task.ExecuteAll(reuseAll);
}
