using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Common.BasicHelper.Utils.Extensions;

namespace Common.BasicHelper.Core.DataStructure.LineBasedPropertyTable;

public static class LbptSerializer
{
    private static readonly List<Type> basicTypes = new()
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

    private static string? SerializedTextAppendText;

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

            var subNode = SerializeProperty(
                target, property, "", sb
            )?.SetParentNode(table.RootNode);

            if (subNode is null) continue;

            table.RootNode.SubNodes.Add(subNode);
        }

        if (SerializedTextAppendText is not null)
            sb.AppendLine(SerializedTextAppendText);

        SerializedTextAppendText = null;

        result = sb.ToString();

        return table;
    }

    private static LineBasedPropertyTableNode? SerializeProperty<T>(
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

        var config = LbptSerializeConfig
            .GetConfigFromAttributes(info.GetCustomAttributes())
            .OnLbptComment((commentAttribute, config) =>
            {
                var lines = commentAttribute?.Comment?.Split('\n');

                if (false
                    || lines is null
                    || (config?.LbptFormatAttribute?.SerializeAsFinalMultilineProperty ?? false)
                ) return;

                foreach (var line in lines)
                {
                    sb.AppendLine($"# {line}");
                }
            })
            .OnLbptFormat((formatAttribute, config) =>
            {

            })
            .Act();

        if (config.LbptFormatAttribute?.Ignore ?? false)
            return null;

        bool isDirectyleSerializable(Type type, out Type? nullableUnderlyingType)
        {
            var isBasicType = basicTypes.Contains(info.PropertyType);
            var isNullableType = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

            if (isNullableType)
                nullableUnderlyingType = Nullable.GetUnderlyingType(type);
            else nullableUnderlyingType = null;

            return isBasicType || isNullableType;
        }

        bool IsEnumerable(object value, out IEnumerable? enumerable, out Type? elementType)
        {
            if (value is IEnumerable enumerableValue)
            {
                enumerable = enumerableValue;
                elementType = ReflectionUtils.GetIEnumerableElementType(info.PropertyType);
                return true;
            }
            else
            {
                enumerable = null;
                elementType = null;
                return false;
            }
        }

        void SerializeNodeToFinalText()
        {
            if (node is null) return;

            if (config?.LbptFormatAttribute?.SerializeAsFinalMultilineProperty ?? false)
            {
                var sb = new StringBuilder();

                var commentLines = config?.LbptCommentAttribute?.Comment;

                if (commentLines is not null)
                    foreach (var line in commentLines.Split('\n'))
                        sb.AppendLine($"# {line}");

                sb.AppendLine($"{node.PropertyPath}: |");
                sb.AppendLine(node.PropertyValue);
                SerializedTextAppendText = sb.ToString();
            }
            else if (config?.LbptFormatAttribute?.SerializeInMultiLineFormat ?? false)
            {
                var beginLine = $"{node.PropertyPath} Began";
                var endedLine = $"{node.PropertyPath} Ended";
                sb.AppendLine(beginLine);
                sb.AppendLine("-".Repeat(beginLine.Length));
                sb.AppendLine(node.PropertyValue);
                sb.AppendLine("-".Repeat(beginLine.Length));
                sb.AppendLine(endedLine);
            }
            else
            {
                sb.AppendLine($"{node.PropertyPath}: {node.PropertyValue}");
            }
        }

        if (IsEnumerable(value, out var enumerable, out var elementType) && value is not string)
        {
            node.IsEnumerable = true;

            var index = 0;

            var castMethod = typeof(Enumerable).GetMethod("Cast")
                .MakeGenericMethod(elementType);
            var castedEnumerable = (IEnumerable)castMethod.Invoke(
                null,
                new[] { enumerable }
            );

            if (isDirectyleSerializable(elementType!, out _))
            {
                foreach (var item in castedEnumerable)
                {
                    node.SubNodes.Add(new()
                    {
                        ParentNode = node,
                        PropertyPath = $"{basePath}[{index}]",
                        PropertyValue = item.ToString()
                    });

                    SerializeNodeToFinalText();

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

                        var subNode = SerializeProperty(
                            item, property, $"{basePath}[{index}]", sb
                        )?.SetParentNode(node);

                        if (subNode is null) continue;

                        node.SubNodes.Add(subNode);
                    }
                    ++index;
                }
            }
        }
        else
        {
            if (isDirectyleSerializable(info.PropertyType, out _))
            {
                node.PropertyValue = value.ToString();

                SerializeNodeToFinalText();
            }
            else
            {
                var properties = info.PropertyType.GetProperties();

                foreach (var property in properties)
                {
                    if (!property.CanRead) continue;

                    var subNode = SerializeProperty(
                        info.GetValue(target), property, basePath, sb
                    )?.SetParentNode(node);

                    if (subNode is null) continue;

                    node.SubNodes.Add(subNode);
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
