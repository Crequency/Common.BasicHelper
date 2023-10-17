using System;
using System.Collections;
using System.Collections.Generic;

namespace Common.BasicHelper.Core;

public class ReflectionUtils
{
    public static bool IsEnumerable(Type type)
    {
        return typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string);
    }

    public static Type GetIEnumerableElementType(Type type)
    {
        Type[] interfaces = type.GetInterfaces();

        foreach (Type iface in interfaces)
        {
            if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return iface.GetGenericArguments()[0];
            }
        }

        return typeof(object);
    }
}
