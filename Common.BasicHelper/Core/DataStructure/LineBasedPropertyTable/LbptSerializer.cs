using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common.BasicHelper.Core.DataStructure.LineBasedPropertyTable;

public static class LbptSerializer
{
    private static List<Type> basicTypes = new()
    {
        typeof(sbyte),
        typeof(byte),
        typeof(short), // Int16
        typeof(ushort), // UInt16
        typeof(int), // Int32
        typeof(uint), // UInt32
        typeof(long), // Int64
        typeof(ulong), // UInt64
        typeof(float),
        typeof(double),
        typeof(decimal),
        typeof(bool),
        typeof(char),
        typeof(string),
        typeof(DateTime),
        typeof(DateTimeOffset),
        typeof(Guid),
        typeof(TimeSpan),
        typeof(Enum),

        typeof(sbyte[]),
        typeof(byte[]),
        typeof(short[]), // Int16[]
        typeof(ushort[]), // UInt16[]
        typeof(int[]), // Int32[]
        typeof(uint[]), // UInt32[]
        typeof(long[]), // Int64[]
        typeof(ulong[]), // UInt64[]
        typeof(float[]),
        typeof(double[]),
        typeof(decimal[]),
        typeof(bool[]),
        typeof(char[]),
        typeof(string[]),
        typeof(DateTime[]),
        typeof(DateTimeOffset[]),
        typeof(Guid[]),
        typeof(TimeSpan[]),
        typeof(Enum[]),
    };

    public static LineBasedPropertyTable Serialize<T>(T target, out string result)
    {
        var table = new LineBasedPropertyTable
        {
            RootNode = new()
        };

        var sb = new StringBuilder();

        var type = typeof(T);

        var properties = type.GetProperties();

        foreach (var property in properties)
        {
            if (!property.CanRead) continue;

            table.RootNode.SubNodes.Add(
                SerializeProperty(
                    target, property, "", sb
                ).SetParentNode(table.RootNode)
            );
        }

        result = sb.ToString();

        return table;
    }

    private static LineBasedPropertyTableNode SerializeProperty<T>(
        T target,
        PropertyInfo info,
        string basePath,
        StringBuilder sb)
    {
        var node = new LineBasedPropertyTableNode();

        basePath = $"{basePath}{(string.IsNullOrWhiteSpace(basePath) ? "" : ".")}{info.Name}";

        node.PropertyName = info.Name;
        node.PropertyPath = basePath;

        var value = info.GetValue(target);

        var attributes = info.GetCustomAttributes();
        foreach (var attribute in attributes)
        {
            if (attribute is LbptCommentAttribute commentAttribute)
            {
                sb.AppendLine($"# {commentAttribute.Comment}");
            }
        }

        if (value is IEnumerable enumerable && value is not string)
        {
            node.IsEnumerable = true;

            var index = 0;

            var elementType = ReflectionUtils.GetIEnumerableElementType(info.PropertyType);
            var castMethod = typeof(Enumerable).GetMethod("Cast")
                .MakeGenericMethod(elementType);
            var castedEnumerable = (IEnumerable)castMethod.Invoke(
                null,
                new[] { enumerable }
            );

            if (basicTypes.Contains(elementType))
            {
                foreach (var item in castedEnumerable)
                {
                    node.SubNodes.Add(new()
                    {
                        ParentNode = node,
                        PropertyPath = $"{basePath}[{index}]",
                        PropertyValue = item.ToString()
                    });
                    sb.AppendLine($"{node.PropertyPath}: {node.PropertyValue}");
                    ++index;
                }
            }
            else
            {
                foreach (var item in castedEnumerable)
                {
                    var type = item.GetType();

                    var properties = type.GetProperties();

                    foreach (var property in properties)
                    {
                        if (!property.CanRead) continue;

                        node.SubNodes.Add(
                            SerializeProperty(
                                item, property, $"{basePath}[{index}]", sb
                            ).SetParentNode(node)
                        );
                    }
                    ++index;
                }
            }
        }
        else
        {
            if (basicTypes.Contains(info.PropertyType))
            {
                node.PropertyValue = value.ToString();
                sb.AppendLine($"{node.PropertyPath}: {node.PropertyValue}");
            }
            else
            {
                var properties = info.PropertyType.GetProperties();

                foreach (var property in properties)
                {
                    if (!property.CanRead) continue;

                    node.SubNodes.Add(
                        SerializeProperty(
                            info.GetValue(target), property, basePath, sb
                        ).SetParentNode(node)
                    );
                }
            }
        }

        return node;
    }

    public static T Deserialize<T>(string value) where T : new()
    {
        throw new NotImplementedException();
    }
}
