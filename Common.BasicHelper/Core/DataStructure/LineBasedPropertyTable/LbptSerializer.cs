using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Common.BasicHelper.Utils.Extensions;

namespace Common.BasicHelper.Core.DataStructure.LineBasedPropertyTable;

public static class LbptSerializer
{
    private static readonly List<Type> BasicTypes =
    [
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
    ];

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
            sb.Append(SerializedTextAppendText);

        SerializedTextAppendText = null;

        result = sb.ToString();

        table.SerializedText = sb.ToString();

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

        try
        {
            // Avoid indexiable property temporary
            // ToDo: indexiable property support
            _ = info.GetValue(target);
        }
        catch
        {
            return null;
        }

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

        void SerializeNodeToFinalText(LineBasedPropertyTableNode node)
        {
            if (node is null) return;

            if (config?.LbptFormatAttribute?.SerializeAsFinalMultilineProperty ?? false)
            {
                var tsb = new StringBuilder();

                var commentLines = config?.LbptCommentAttribute?.Comment;

                if (commentLines is not null)
                    foreach (var line in commentLines.Split('\n'))
                        tsb.AppendLine($"# {line}");

                tsb.AppendLine($"{node.PropertyPath}: |");
                tsb.Append(node.PropertyValue);

                SerializedTextAppendText = tsb.ToString();
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

        if (ReflectionUtils.IsEnumerable(
                value, out var enumerable, out var elementType, info
            ) && value is not string)
        {
            node.IsEnumerable = true;

            var index = 0;

            var castMethod = typeof(Enumerable)
                .GetMethod("Cast")
                .MakeGenericMethod(elementType);

            var castedEnumerable = (IEnumerable)castMethod.Invoke(
                null,
                new[] { enumerable }
            );

            if (IsTypeDirectlySerializable(elementType!, out _))
            {
                foreach (var item in castedEnumerable)
                {
                    var subNode = new LineBasedPropertyTableNode()
                    {
                        ParentNode = node,
                        PropertyPath = $"{basePath}[{index}]",
                        PropertyValue = item.ToString()
                    };

                    node.SubNodes.Add(subNode);

                    SerializeNodeToFinalText(subNode);

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
            if (IsPropertyDirectlySerializable(info.PropertyType, out _, info))
            {
                if (value is null)
                    node.PropertyValue = "Null";
                else
                    node.PropertyValue = value.ToString();

                SerializeNodeToFinalText(node);
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

    private static Dictionary<string, string> DeserializationParse(string text)
    {
        var singleLinePropertyRegex = @"(?:^|\r?\n)(\S*): ([\S ]*)(?:\r?\n|$)";
        var multiLinePropertyRegex = @"(?:^|\r?\n)(\S*) Began\n(-*)\n([\S\s]*)\n(-*)\n(\S*) Ended(?:\r?\n|$)";
        var multiLineFinalPropertyRegex = @"(?:^|\r?\n)(\S*): \|\n([\S\s]*)$";

        var propertiesValuesDict = new Dictionary<string, string>();

        var multiLineFinalPropertyMatches = Regex.Matches(text, multiLineFinalPropertyRegex);

        if (multiLineFinalPropertyMatches.Count() > 1)
            throw new ArgumentException(
                $"Final property up to 1 while currently {multiLineFinalPropertyMatches.Count()} was gave.",
                nameof(text)
            );

        if (multiLineFinalPropertyMatches.Count() == 1)
        {
            var groups = multiLineFinalPropertyMatches[0].Groups;

            propertiesValuesDict.Add(groups[1].Value, groups[2].Value);
        }

        var singleLinePropertyMatches = Regex.Matches(text, singleLinePropertyRegex, RegexOptions.Multiline);
        var multiLinePropertyMatches = Regex.Matches(text, multiLinePropertyRegex, RegexOptions.Multiline);

        for (var i = 0; i < singleLinePropertyMatches.Count(); i++)
        {
            var property = singleLinePropertyMatches[i];

            if (property.Length == 0) continue;
            if (property.Value.EndsWith("|\n") || property.Value.EndsWith("|\r\n")) continue;

            var groups = property.Groups;

            propertiesValuesDict.Add(groups[1].Value, groups[2].Value);
        }

        for (var i = 0; i < multiLinePropertyMatches.Count(); i++)
        {
            var property = multiLinePropertyMatches[i];

            if (property.Length == 0) continue;

            var groups = property.Groups;

            if (!groups[1].Value.Equals(groups[5].Value))
                throw new FormatException("Begining property path not equals to ending property path.");

            var separatorCountsEquals = groups[2].Value.Length == groups[4].Value.Length;
            var separatorCountsMatchs = groups[1].Value.Length + 6 == groups[2].Value.Length;
            if (!separatorCountsEquals || !separatorCountsMatchs)
                throw new FormatException("Separator counts not equals to stantard.");

            propertiesValuesDict.Add(groups[1].Value, groups[3].Value);
        }

        return propertiesValuesDict;
    }

    public static T? Deserialize<T>(string? text) where T : class, new()
    {
        if (text is null) return null;

        var propertiesValuesDict = DeserializationParse(text);

        var table = new LineBasedPropertyTable();

        var deserializedObject = new T();

        var type = typeof(T);

        var properties = type.GetProperties();

        foreach (var property in properties)
        {
            if (!property.CanWrite) continue;

            DeserializeProperty(
                deserializedObject,
                "",
                propertiesValuesDict,
                property,
                ref table
            );
        }

        return deserializedObject;
    }

    private static void DeserializeProperty<T>(
        T target,
        string basePath,
        Dictionary<string, string> propertiesValues,
        PropertyInfo info,
        ref LineBasedPropertyTable table)
    {
        var node = new LineBasedPropertyTableNode();

        basePath = $"{basePath}{(string.IsNullOrWhiteSpace(basePath) ? "" : ".")}{info.Name}";

        var config = LbptSerializeConfig
            .GetConfigFromAttributes(info.GetCustomAttributes())
            .Act();

        if (config.LbptFormatAttribute?.Ignore ?? false)
            return;

        node.PropertyName = info.Name;
        node.PropertyPath = basePath;

        if (ReflectionUtils.IsEnumerable(info.PropertyType, out var elementType) && !info.PropertyType.IsAssignableFrom(typeof(string)))
        {
            if (elementType is null) return;

            if (IsTypeDirectlySerializable(elementType, out var type))
            {
                var listType = typeof(List<>).MakeGenericType(elementType);
                var collection = Activator.CreateInstance(listType);
                var addMethod = collection.GetType().GetMethod("Add");

                var regex = $"{Regex.Escape(basePath)}\\[(\\d)\\]";

                var match = propertiesValues.Where(
                    x => Regex.IsMatch(x.Key, regex)
                );

                foreach (var item in match)
                    addMethod?.Invoke(
                        collection,
                        new[]
                        {
                            ReflectionUtils.ParseValue(
                                elementType,
                                item.Value,
                                typeof(string)
                            )
                        }
                    );

                info.SetValue(target, collection);
            }
            else
            {

            }
        }
        else
        {
            if (IsPropertyDirectlySerializable(info.PropertyType, out _, info))
            {
                info.SetValue(
                    target,
                    ReflectionUtils.ParseValue(
                        info.PropertyType,
                        propertiesValues[basePath],
                        typeof(string)
                    )
                );
            }
            else
            {
                var properties = info.PropertyType.GetProperties();

                foreach (var property in properties)
                {
                    if (!property.CanWrite) continue;

                    DeserializeProperty(target, basePath, propertiesValues, property, ref table);
                }
            }
        }
    }

    private static bool IsPropertyDirectlySerializable(Type type, out Type? nullableUnderlyingType, PropertyInfo info)
    {
        var isBasicType = BasicTypes.Contains(info.PropertyType);
        var isNullableType = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

        if (isNullableType)
            nullableUnderlyingType = Nullable.GetUnderlyingType(type);
        else nullableUnderlyingType = null;

        return isBasicType || isNullableType;
    }

    private static bool IsTypeDirectlySerializable(Type type, out Type? nullableUnderlyingType)
    {
        var isBasicType = BasicTypes.Contains(type);
        var isNullableType = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

        if (isNullableType)
            nullableUnderlyingType = Nullable.GetUnderlyingType(type);
        else nullableUnderlyingType = null;

        return isBasicType || isNullableType;
    }
}
