using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BasicHelper.Math;

namespace BasicHelper.Util
{
    public static class GUID_Helper
    {
        /// <summary>
        /// 检查格式
        /// </summary>
        /// <param name="part">部分GUID</param>
        /// <returns>是否合法</returns>
        public static bool FormatCheck(GUID_Part part)
        {
            string tmp = part.GetString();
            for (int i = 0; i < 5; i++)
                if (!Regex.IsMatch(tmp[i].ToString(), RegexStrings.GUID_Part_Parttern))
                    return false;
            return true;
        }

        /// <summary>
        /// 生成一个随机字符, 但符合 [RegexStrings].GUID_Part_Parttern
        /// </summary>
        /// <returns>一个随机字符</returns>
        public static char RandomCharGenerate()
        {
            char rst = (char)('0' - 1);
            while (!Regex.IsMatch(rst.ToString(), RegexStrings.GUID_Part_Parttern))
                rst = (char)new Random().Next(Standard.Min('a', 'A', '0'), Standard.Max('z', 'Z', '9'));
            return rst;
        }

        /// <summary>
        /// 生成一个随机部分GUID, 字符来源 RandomCharGenerate() 函数
        /// </summary>
        /// <returns>一个部分GUID</returns>
        public static GUID_Part Random_GUID_Part_Generate() => new(string.Concat(RandomCharGenerate(),
            RandomCharGenerate(), RandomCharGenerate(), RandomCharGenerate(), RandomCharGenerate()));

        /// <summary>
        /// 生成一个随机GUID, 部分GUID来源 Random_GUID_Part_Generate() 函数
        /// </summary>
        /// <returns>一个GUID</returns>
        public static GUID Random_GUID_Generate() => new(
            $"{Random_GUID_Part_Generate().GetString()}-" +
            $"{Random_GUID_Part_Generate().GetString()}-" +
            $"{Random_GUID_Part_Generate().GetString()}-" +
            $"{Random_GUID_Part_Generate().GetString()}-" +
            $"{Random_GUID_Part_Generate().GetString()}"
        );
    }

    public struct GUID
    {
        private readonly GUID_Part[] parts = new GUID_Part[5];

        public GUID_Part A { get => parts[0]; set => parts[0] = value; }

        public GUID_Part B { get => parts[1]; set => parts[1] = value; }

        public GUID_Part C { get => parts[2]; set => parts[2] = value; }

        public GUID_Part D { get => parts[3]; set => parts[3] = value; }

        public GUID_Part E { get => parts[4]; set => parts[4] = value; }

        /// <summary>
        /// GUID的构造函数
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="sep">分隔符, 默认为'-'</param>
        /// <exception cref="Result{bool}">格式错误异常</exception>
        public GUID(string input, char sep = '-')
        {
            string[] tmp = input.Split(sep);
            if (tmp.Length != 5) throw new Result<bool>("Error input format!");
            for (int i = 0; i < 5; i++)
                parts[i] = GUID_Part.BuildFromString(tmp[i]);
        }

        /// <summary>
        /// 从字符串构造GUID
        /// </summary>
        /// <param name="input">输入数据</param>
        /// <param name="sep">数据分隔符, 默认为'-'</param>
        /// <returns>一个GUID</returns>
        public static GUID BuildFromString(string input, char sep = '-') => new(input, sep);

        /// <summary>
        /// 返回字符串
        /// </summary>
        /// <param name="sep">分隔符, 默认为'-'</param>
        /// <returns>字符串</returns>
        public string GetString(char sep = '-')
        {
            StringBuilder sb = new();
            for (int i = 0; i < 5; i++)
                sb.Append($"{parts[i].GetString()}{(i == 4 ? "" : sep)}");
            return sb.ToString();
        }
    }

    public struct GUID_Part
    {
        private readonly char[] ids = new char[5];

        public char A { get => ids[0]; set => ids[0] = value; }

        public char B { get => ids[1]; set => ids[1] = value; }

        public char C { get => ids[2]; set => ids[2] = value; }

        public char D { get => ids[3]; set => ids[3] = value; }

        public char E { get => ids[4]; set => ids[4] = value; }

        /// <summary>
        /// 部分GUID的构造函数
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <exception cref="Result{bool}">构造失败, 格式错误</exception>
        public GUID_Part(string input)
        {
            if (input.Length != 5)
                throw new Result<bool>("Error argument length.");
            else
                for (int i = 0; i < 5; i++)
                    ids[i] = input[i];
            if (!GUID_Helper.FormatCheck(this))
                throw new Result<bool>("Error guid_part format.");
        }

        /// <summary>
        /// 从字符串中构造
        /// </summary>
        /// <param name="input">字符串</param>
        /// <returns>部分GUID</returns>
        public static GUID_Part BuildFromString(string input) => new(input);

        /// <summary>
        /// 返回字符串
        /// </summary>
        /// <returns>字符串</returns>
        public string GetString()
        {
            StringBuilder sb = new();
            for (int i = 0; i < 5; i++)
                sb.Append(ids[i]);
            return sb.ToString().ToUpper();
        }
    }
}
