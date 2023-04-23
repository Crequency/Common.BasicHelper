using System.Text;

namespace Common.BasicHelper.Utils.Extensions;

public static class BinaryHelper
{
    /// <summary>
    /// 二进制内容转 UTF8 字符串
    /// </summary>
    /// <param name="content">字节数组</param>
    /// <param name="index">内容起始下标</param>
    /// <param name="count">内容长度</param>
    /// <returns>UTF8 字符串</returns>
    public static string ToUTF8(this byte[] content, int? index = null, int? count = null)
    {
        if (index is null || count is null)
            return Encoding.UTF8.GetString(content);
        else
            return Encoding.UTF8.GetString(
                content,
                index ?? 0,
                count ?? content.Length
            );
    }

    /// <summary>
    /// 二进制内容转 UTF32 字符串
    /// </summary>
    /// <param name="content">字节数组</param>
    /// <param name="index">内容起始下标</param>
    /// <param name="count">内容长度</param>
    /// <returns>UTF32 字符串</returns>
    public static string ToUTF32(this byte[] content, int? index = null, int? count = null)
    {
        if (index is null || count is null)
            return Encoding.UTF32.GetString(content);
        else
            return Encoding.UTF32.GetString(
                content,
                index ?? 0,
                count ?? content.Length
            );
    }

    /// <summary>
    /// 二进制内容转 Unicode 字符串
    /// </summary>
    /// <param name="content">字节数组</param>
    /// <param name="index">内容起始下标</param>
    /// <param name="count">内容长度</param>
    /// <returns>Unicode 字符串</returns>
    public static string ToUnicode(this byte[] content, int? index = null, int? count = null)
    {
        if (index is null || count is null)
            return Encoding.Unicode.GetString(content);
        else
            return Encoding.Unicode.GetString(
                content,
                index ?? 0,
                count ?? content.Length
            );
    }

    /// <summary>
    /// 二进制内容转 ASCII 字符串
    /// </summary>
    /// <param name="content">字节数组</param>
    /// <param name="index">内容起始下标</param>
    /// <param name="count">内容长度</param>
    /// <returns>ASCII 字符串</returns>
    public static string ToASCII(this byte[] content, int? index = null, int? count = null)
    {
        if (index is null || count is null)
            return Encoding.ASCII.GetString(content);
        else
            return Encoding.ASCII.GetString(
                content,
                index ?? 0,
                count ?? content.Length
            );
    }
}
