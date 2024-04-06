using System;
using System.Text.RegularExpressions;

namespace Common.BasicHelper.Utils.Extensions;

public static class RegexHelper
{
    public static Match? WhenSuccess(this Match? match, Action<Match?> action)
    {
        if (match?.Success ?? false) action?.Invoke(match);

        return match;
    }
}
