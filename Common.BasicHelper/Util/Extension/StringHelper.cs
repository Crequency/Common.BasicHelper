using System.Text;

namespace Common.BasicHelper.Util.Extension;

public static class StringHelper
{
    public static char[] A_Z = new char[26]
    {
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N',
        'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
    };

    /// <summary>
    /// 将数字转换成大写字母
    /// </summary>
    /// <param name="source">源字符串</param>
    /// <returns>转换后字符串</returns>
    public static string Num2UpperChar(this string source)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < source.Length; ++i)
            if (source[i] >= '0' && source[i] <= '9')
                sb.Append(A_Z[source[i] - '0']);
            else sb.Append(source[i]);
        return sb.ToString();
    }

}
