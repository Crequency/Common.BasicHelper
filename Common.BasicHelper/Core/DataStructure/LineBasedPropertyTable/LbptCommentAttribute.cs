using System;

namespace Common.BasicHelper.Core.DataStructure.LineBasedPropertyTable;

[AttributeUsage(AttributeTargets.Property)]
public class LbptCommentAttribute(string? comment = null) : Attribute
{
    public string? Comment { get; } = comment;
}

