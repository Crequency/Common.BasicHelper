using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        var isEnumerable = type.GetInterfaces()
            .Any(
                x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEnumerable<>)
            ) && type != typeof(string);
        //var isEnumerable = type.IsAssignableFrom(typeof(IEnumerable)) && type != typeof(string);

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

    public static dynamic ParseValue(Type type, object value, Type valueType)
    {
        if (type == valueType) return value;

        var parseMethod = type.GetMethod("Parse", new[] { valueType })
            ?? throw new ArgumentOutOfRangeException(
                nameof(value), $"Cannot find method `Parse` for type {valueType.Name}."
            );

        if (parseMethod.IsStatic)
        {
            return parseMethod.Invoke(null, new[] { value });
        }
        else
        {
            throw new Exception("Cannot execute non-static `Parse` function.");
        }
    }
}
