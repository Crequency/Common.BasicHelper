using System;
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
}
