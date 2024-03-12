using System;
using System.Collections.Generic;

namespace Common.BasicHelper.Core.DataStructure.LineBasedPropertyTable;

internal class LbptSerializeConfig
{
    internal LbptSerializeConfig Act()
    {
        LbptCommentAction?.Invoke(LbptCommentAttribute, this);
        LbptFormatAction?.Invoke(LbptFormatAttribute, this);

        return this;
    }

    internal LbptCommentAttribute? LbptCommentAttribute { get; set; }

    private Action<LbptCommentAttribute?, LbptSerializeConfig?>? LbptCommentAction { get; set; }

    internal LbptSerializeConfig OnLbptComment(Action<LbptCommentAttribute?, LbptSerializeConfig?> action)
    {
        LbptCommentAction = action;
        return this;
    }

    internal LbptFormatAttribute? LbptFormatAttribute { get; set; }

    private Action<LbptFormatAttribute?, LbptSerializeConfig?>? LbptFormatAction { get; set; }

    internal LbptSerializeConfig OnLbptFormat(Action<LbptFormatAttribute?, LbptSerializeConfig?> action)
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

