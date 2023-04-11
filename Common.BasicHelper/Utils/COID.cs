using Common.BasicHelper.Core.Exceptions;
using Common.BasicHelper.Math;
using Common.BasicHelper.Utils.Extensions;
using System;
using System.Text.RegularExpressions;

namespace Common.BasicHelper.Utils;

public static class COID_Helper
{
    /// <summary>
    /// 从字符串构造 COID 对象
    /// </summary>
    /// <param name="coid_str">COID 字符串</param>
    /// <param name="sep">数据分隔符, 默认为'-'</param>
    /// <returns>一个 COID 对象</returns>
    public static COID Build_COID(string coid_str, char sep = '-') => new(coid_str, sep);

    /// <summary>
    /// 从字符串中构造 COID_Part 对象
    /// </summary>
    /// <param name="coid_part">字符串</param>
    /// <returns>一个 COID_Part 对象</returns>
    public static COID_Part Build_COID_Part(string coid_part) => new(coid_part);

    /// <summary>
    /// 检查格式
    /// </summary>
    /// <param name="part">部分 COID</param>
    /// <returns>是否合法</returns>
    public static bool FormatCheck(COID_Part part)
    {
        var tmp = part.ToString();
        for (int i = 0; i < 5; i++)
            if (!Regex.IsMatch(tmp[i].ToString(), RegexStrings.COID_Part_Parttern))
                return false;
        return true;
    }

    /// <summary>
    /// 生成一个随机字符, 但符合 [RegexStrings].COID_Part_Parttern
    /// </summary>
    /// <returns>一个随机字符</returns>
    public static char RandomCharGenerate()
    {
        var rst = (char)('0' - 1);
        while (!Regex.IsMatch(rst.ToString(), RegexStrings.COID_Part_Parttern))
            rst = (char)new Random().Next(Standard.Min('a', 'A', '0'),
                Standard.Max('z', 'Z', '9'));
        return rst;
    }

    /// <summary>
    /// 生成一个随机 COID, COID_Part 来源 Random_COID_Part_Generate() 函数
    /// </summary>
    /// <returns>一个 COID</returns>
    public static COID Random_COID_Generate() => new()
    {
        A = Random_COID_Part_Generate(),
        B = Random_COID_Part_Generate(),
        C = Random_COID_Part_Generate(),
        D = Random_COID_Part_Generate(),
        E = Random_COID_Part_Generate(),
    };

    /// <summary>
    /// 生成一个随机部分COID, 字符来源 RandomCharGenerate() 函数
    /// </summary>
    /// <returns>一个部分COID</returns>
    public static COID_Part Random_COID_Part_Generate() => new(
        string.Concat(
            RandomCharGenerate(),
            RandomCharGenerate(),
            RandomCharGenerate(),
            RandomCharGenerate(),
            RandomCharGenerate()
            )
        );
}

public class COID
{
    private readonly COID_Part[] parts = new COID_Part[5];

    public COID_Part A { get => parts[0]; set => parts[0] = value; }

    public COID_Part B { get => parts[1]; set => parts[1] = value; }

    public COID_Part C { get => parts[2]; set => parts[2] = value; }

    public COID_Part D { get => parts[3]; set => parts[3] = value; }

    public COID_Part E { get => parts[4]; set => parts[4] = value; }

    public COID()
    {

    }

    /// <summary>
    /// COID 的构造函数
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <param name="sep">分隔符, 默认为 '-'</param>
    /// <exception cref="ArgumentException">参数长度错误异常</exception>
    public COID(string input, char sep = '-')
    {
        parts = new COID_Part[5];
        var tmp = input.Split(sep);

        if (tmp.Length != 5)
            ErrorCodes.CB0033
                .BuildMessage(inputFormatRequirements: "Need 5 parts.")
                .Throw<ArgumentException>();

        for (int i = 0; i < 5; i++)
            parts[i] = new(tmp[i]);
    }

    /// <summary>
    /// 返回字符串
    /// </summary>
    /// <param name="sep">分隔符, 默认为'-'</param>
    /// <returns>字符串</returns>
    public string GetString(char sep = '-') => $"{A}{sep}{B}{sep}{C}{sep}{D}{sep}{E}";

    /// <summary>
    /// 重写 ToString() 方法, 返回形如 `P831N-MQV15-2D6UC-KKWXH-3IHI7` 的字符串
    /// </summary>
    /// <returns>字符串</returns>
    public override string ToString() => $"{A}-{B}-{C}-{D}-{E}";

    /// <summary>
    /// 重载判等方法
    /// </summary>
    /// <param name="obj">判等对象</param>
    /// <returns>是否相等</returns>
    public override bool Equals(object obj) => ToString().Equals(obj.ToString());

    /// <summary>
    /// 重载哈希方法
    /// </summary>
    /// <returns>哈希值</returns>
    public override int GetHashCode() => ToString().GetHashCode();
}

public class COID_Part
{
    private readonly char[] ids = new char[5];

    public char A { get => ids[0]; set => ids[0] = value; }

    public char B { get => ids[1]; set => ids[1] = value; }

    public char C { get => ids[2]; set => ids[2] = value; }

    public char D { get => ids[3]; set => ids[3] = value; }

    public char E { get => ids[4]; set => ids[4] = value; }

    public COID_Part()
    {

    }

    /// <summary>
    /// COID_Part 的构造函数
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <exception cref="ArgumentException">参数长度错误</exception>
    /// <exception cref="FormatException">COID_Part 格式错误</exception>
    public COID_Part(string input)
    {
        ids = new char[5];

        if (input.Length != 5)
            ErrorCodes.CB0034
                .BuildMessage(inputLengthRequirements: "It should be 5 chars.")
                .Throw<ArgumentException>();
        else
            for (int i = 0; i < 5; i++)
                ids[i] = input[i];

        if (!COID_Helper.FormatCheck(this))
            ErrorCodes.CB0033.BuildMessage().Throw<FormatException>();
    }

    /// <summary>
    /// 重写 ToString() 方法, 返回形如 `2D6UC` 的字符串
    /// </summary>
    /// <returns>字符串</returns>
    public override string ToString() => $"{A}{B}{C}{D}{E}".ToUpper();
}
