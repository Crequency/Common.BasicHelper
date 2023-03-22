using System.Collections.Generic;
using System.Text;

namespace Common.BasicHelper.Utils.Extensions;

public static class ListHelper
{
    /// <summary>
    /// 转换到自定义字符串
    /// </summary>
    /// <typeparam name="T">列表类型</typeparam>
    /// <param name="list">列表</param>
    /// <param name="separater">分隔符</param>
    /// <returns>自定义字符串</returns>
    public static string ToCustomString<T>(this List<T> list, string separater = ",")
    {
        var sb = new StringBuilder();

        foreach (var item in list)
        {
            sb.Append(item.ToString());
            sb.Append(separater);
        }

        return sb.ToString();
    }

}
