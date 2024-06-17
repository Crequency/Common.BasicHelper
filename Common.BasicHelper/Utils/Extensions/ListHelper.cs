using System.Collections.Generic;
using System.Text;

namespace Common.BasicHelper.Utils.Extensions;

public static class ListHelper
{
    public static string ToCustomString<T>(this List<T> list, string separator = ",", bool cutEnding = true)
    {
        var sb = new StringBuilder();

        foreach (var item in list)
        {
            sb.Append(item);
            sb.Append(separator);
        }

        var result = sb.ToString();

        return cutEnding ? result[..^separator.Length] : result;
    }

}
