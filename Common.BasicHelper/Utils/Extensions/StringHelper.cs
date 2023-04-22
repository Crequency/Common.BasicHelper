using Common.BasicHelper.IO;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Common.BasicHelper.Utils.Extensions;

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

    /// <summary>
    /// 从磁盘读取全部文本
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>文本内容, 若文件不存在则返回空</returns>
    public static string? ReadAllTextFromDisk(this string path)
    {
        if (File.Exists(path))
            return File.ReadAllText(path);
        else return null;
    }

    /// <summary>
    /// 从磁盘异步读取全部文本
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>文本内容读取任务, 若文件不存在则返回空</returns>
    public static async Task<string?> ReadAllTextFromDiskAsync(this string path)
    {
        if (File.Exists(path))
            return await File.ReadAllTextAsync(path);
        else return null;
    }

    /// <summary>
    /// 对字符串进行按指定数量分组拼接
    /// </summary>
    /// <param name="text">字符串</param>
    /// <param name="count">每组字符数</param>
    /// <param name="action">每组分割完后动作</param>
    /// <param name="executeAfterLastGroup">最后一组分割完成后是否执行动作</param>
    /// <returns>返回拼接结果</returns>
    public static string SeparateGroup
    (
        this string text,
        int count,
        Action<StringBuilder>? action = null,
        bool executeAfterLastGroup = false
    )
    {
        var sb = new StringBuilder();
        for (int i = 0, j = 0; i < text.Length; ++i)
        {
            void normalSeparate()
            {
                sb.Append(text[i]);
                ++j;
            }

            normalSeparate();

            if (j == count)
            {
                if (i != text.Length - 1)
                    action?.Invoke(sb);
                else if (executeAfterLastGroup)
                    action?.Invoke(sb);

                j = 0;
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// 判断字符串是否为空
    /// </summary>
    /// <param name="str">字符串对象</param>
    /// <returns>是否为空</returns>
    public static bool IsNullOrEmpty(this string? str) => string.IsNullOrEmpty(str);

    /// <summary>
    /// 判断字符串是否为空或仅由空白组成
    /// </summary>
    /// <param name="str">字符串对象</param>
    /// <returns>是否为空或仅有空白组成</returns>
    public static bool IsNullOrWhiteSpace(this string? str) => string.IsNullOrWhiteSpace(str);

    /// <summary>
    /// 将字符串作为异常消息抛出
    /// </summary>
    /// <typeparam name="T">异常类型</typeparam>
    /// <param name="message">异常消息</param>
    public static void Throw<T>(this string? message) where T : Exception
    {
        var exp = Activator.CreateInstance(typeof(T), message);
        throw (exp as T) ?? new Exception(message);
    }

    /// <summary>
    /// 获取给定路径的完整路径
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns>完整路径</returns>
    public static string GetFullPath(this string path) => Path.GetFullPath(path);

    /// <summary>
    /// 获取给定路径的完整大小 (字节数)
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns>给定路径的完整字节数</returns>
    public static long GetTotalSize(this string path) => DirectoryHelper.GetDirectorySize(path);
}
