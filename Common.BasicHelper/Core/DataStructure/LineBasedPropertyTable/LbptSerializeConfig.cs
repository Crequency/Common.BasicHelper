using System;
using System.Collections.Generic;

namespace Common.BasicHelper.Core.DataStructure.LineBasedPropertyTable;

internal class LbptSerializeConfig
{
    internal LbptSerializeConfig Act()
    {
        LbptCommentAction?.Invoke(LbptCommentAttribute);
        LbptFormatAction?.Invoke(LbptFormatAttribute);

        return this;
    }

    internal LbptCommentAttribute? LbptCommentAttribute { get; set; }

    private Action<LbptCommentAttribute?>? LbptCommentAction { get; set; }

    internal LbptSerializeConfig OnLbptComment(Action<LbptCommentAttribute?> action)
    {
        LbptCommentAction = action;
        return this;
    }

    internal LbptFormatAttribute? LbptFormatAttribute { get; set; }

    private Action<LbptFormatAttribute?>? LbptFormatAction { get; set; }

    internal LbptSerializeConfig OnLbptFormat(Action<LbptFormatAttribute?> action)
    {
        LbptFormatAction = action;
        return this;
    }

    internal static LbptSerializeConfig GetConfigFromAttributes(IEnumerable<Attribute> attributes)
    {
        var config = new LbptSerializeConfig();

        foreach (var attribute in attributes)
        {
            if (attribute is LbptCommentAttribute lbptCommentAttribute)
            {
                config.LbptCommentAttribute = lbptCommentAttribute;
            }
            else if (attribute is LbptFormatAttribute lbptFormatAttribute)
            {
                config.LbptFormatAttribute = lbptFormatAttribute;
            }
        }

        return config;
    }
}

