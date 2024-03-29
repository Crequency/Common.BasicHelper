﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.BasicHelper.Utils.Extensions;

public static class IEnumerableHelper
{
    public static IEnumerable<T> AssertCount<T>(this IEnumerable<T> items, int count, Exception? exception = null)
    {
        var current = items.Count();

        if (current != count)
            throw exception ?? new ArgumentException(
                $"Count ({current}) not match {count}",
                nameof(items)
            );

        return items;
    }

    public static IEnumerable<T> WhenCount<T>(
        this IEnumerable<T> items,
        Func<int, bool> match,
        Func<T, bool> predicate,
        Action<IEnumerable<T>> action
    )
    {
        var count = 0;

        var execute = false;

        foreach (var item in items)
        {
            if (predicate(item))
                count++;

            if (match(count))
            {
                execute = true;
                break;
            }
        }

        if (execute)
            action(items);

        return items;
    }

    public static int IndexOf<T>(this IEnumerable<T> items, Func<T, bool> match)
    {
        var index = 0;

        foreach (var item in items)
        {
            if (match.Invoke(item))
                return index;

            ++index;
        }

        return -1;
    }
}
