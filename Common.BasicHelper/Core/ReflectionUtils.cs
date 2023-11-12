using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Common.BasicHelper.Core;

public class ReflectionUtils
{
    public static Type GetIEnumerableElementType(Type type)
    {
        var interfaces = type.GetInterfaces();

        foreach (var iface in interfaces)
        {
            if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return iface.GetGenericArguments()[0];
            }
        }

        return typeof(object);
    }

    public static bool IsEnumerable(Type type, out Type? elementType)
    {
        var isEnumerable = typeof(IEnumerable).IsAssignableFrom(type) && type != typeof(string);

        elementType = GetIEnumerableElementType(type);

        return isEnumerable;
    }

    public static bool IsEnumerable(object value, out IEnumerable? enumerable, out Type? elementType, PropertyInfo info)
    {
        if (value is IEnumerable enumerableValue)
        {
            enumerable = enumerableValue;
            elementType = GetIEnumerableElementType(info.PropertyType);
            return true;
        }
        else
        {
            enumerable = null;
            elementType = null;
            return false;
        }
    }
}
