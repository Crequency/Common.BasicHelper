using Common.BasicHelper.IO;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.BasicHelper.Utils.Extensions;

public static class StringHelper
{
    public static char[] A_Z =
    [
        'A',
        'B',
        'C',
        'D',
        'E',
        'F',
        'G',
        'H',
        'I',
        'J',
        'K',
        'L',
        'M',
        'N',
        'O',
        'P',
        'Q',
        'R',
        'S',
        'T',
        'U',
        'V',
        'W',
        'X',
        'Y',
        'Z'
    ];

    public static string Num2UpperChar(this string source)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < source.Length; ++i)
            if (source[i] >= '0' && source[i] <= '9')
                sb.Append(A_Z[source[i] - '0']);
            else sb.Append(source[i]);
        return sb.ToString();
    }

    public static string? ReadAllTextFromDisk(this string path)
    {
        if (File.Exists(path))
            return File.ReadAllText(path);
        else return null;
    }

    public static async Task<string?> ReadAllTextFromDiskAsync(this string path)
    {
        if (File.Exists(path))
            return await File.ReadAllTextAsync(path);
        else return null;
    }

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

    public static bool IsNullOrEmpty(this string? str) => string.IsNullOrEmpty(str);

    public static bool IsNullOrWhiteSpace(this string? str) => string.IsNullOrWhiteSpace(str);

    public static void Throw<T>(this string? message) where T : Exception
    {
        var exp = Activator.CreateInstance(typeof(T), message);
        throw (exp as T) ?? new Exception(message);
    }

    public static string GetFullPath(this string path) => Path.GetFullPath(path);

    public static long GetTotalSize(this string path) => DirectoryHelper.GetDirectorySize(path);

    public static byte[] FromUTF8(this string text) => Encoding.UTF8.GetBytes(text);

    public static byte[] FromUTF32(this string text) => Encoding.UTF32.GetBytes(text);

    public static byte[] FromUnicode(this string text) => Encoding.Unicode.GetBytes(text);

    public static byte[] FromASCII(this string text) => Encoding.ASCII.GetBytes(text);

    public static string Repeat(this string src, int count)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < count; ++i)
            sb.Append(src);
        return sb.ToString();
    }

    public static bool IsAscii(this string text) => text.Any(x => x is not (>= (char)0 and <= (char)0xff)) == true;
}
