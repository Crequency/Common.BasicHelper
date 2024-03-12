using System;

namespace Common.BasicHelper.Core.DataStructure.LineBasedPropertyTable;

[AttributeUsage(AttributeTargets.Property)]
public class LbptFormatAttribute : Attribute
{
    public bool Ignore { get; set; } = false;

    public bool SerializeInMultiLineFormat { get; set; } = false;

    public bool SerializeAsFinalMultilineProperty { get; set; } = false;
}

