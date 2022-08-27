using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BasicHelper.Util
{
    public struct Version
    {

        private ushort majorVersion;
        private ushort minorVersion;
        private ushort branchVersion;
        private ushort buildVersion;

        public List<Architecture> SupportedArchitectures => new List<Architecture>();

        public List<Platform> SupportedPlatforms => new List<Platform>();

        /// <summary>
        /// 设置版本
        /// </summary>
        /// <param name="major">主版本号</param>
        /// <param name="minor">副版本号</param>
        /// <param name="branch">分支版本号</param>
        /// <param name="build">构建版本号</param>
        public void SetVersion(ushort major, ushort minor, ushort branch, ushort build)
        {
            majorVersion = major;
            minorVersion = minor;
            branchVersion = branch;
            buildVersion = build;
        }

        /// <summary>
        /// 增加一个受支持的架构
        /// </summary>
        /// <param name="arch">架构</param>
        public void AddSupportedArchitecture(Architecture arch) => SupportedArchitectures.Add(arch);

        /// <summary>
        /// 增加一个受支持的平台
        /// </summary>
        /// <param name="platform">平台</param>
        public void AddPlatform(Platform platform) => SupportedPlatforms.Add(platform);

        /// <summary>
        /// 版本类别
        /// </summary>
        public enum Type
        {
            Debug = 0, Preview = 1,
            Release = 2, Latest = 3
        }

        /// <summary>
        /// 版本状态
        /// </summary>
        public enum State
        {
            Developing = 0, Testing = 1,
            Alpha = 2, Beta = 3,
            Normal = 4,
            LTS = 5, // Long time surpport, 长期支持
            Stopped = 6, // Stopped surpport and updating, 停止更新与支持
        }

        /// <summary>
        /// 运行架构
        /// </summary>
        public enum Architecture
        {
            x86_32 = 0,
            x86_64 = 1,
            arm = 11,
            long_arch = 21,
        }

        /// <summary>
        /// 主版本号
        /// </summary>
        public ushort Major => majorVersion;

        /// <summary>
        /// 副版本号
        /// </summary>
        public ushort Minor => minorVersion;

        /// <summary>
        /// 分支版本号
        /// </summary>
        public ushort Branch => branchVersion;

        /// <summary>
        /// 构建版本号
        /// </summary>
        public ushort Build => buildVersion;

        /// <summary>
        /// 获取版本号字符串
        /// </summary>
        /// <param name="prefix">前导字符串</param>
        /// <param name="suffix">后缀字符串</param>
        /// <returns>版本号字符串</returns>
        public string GetVersionText(string prefix = "v", string suffix = "")
        {
            return
                $"{prefix}" +
                $"{Major}.{Minor}.{Branch}.{Build}" +
                $"{suffix}";
        }

        /// <summary>
        /// 从字符串反序列化版本结构
        /// </summary>
        /// <param name="version">字符串</param>
        /// <returns>版本结构</returns>
        public static Version Parse(string version)
        {
            Regex regex = new Regex(RegexStrings.Version_Parse_STR);
            MatchCollection collection = regex.Matches(version);
            if (collection.Count > 1)
                throw new Result<bool>("More than one version expression matched.");
            else
            {
                foreach (Match match in collection.Cast<Match>())
                {
                    string[] parts = match.Value.Split('.');
                    Version result = new Version();
                    result.SetVersion(
                        ushort.Parse(parts[0]),
                        ushort.Parse(parts[1]),
                        ushort.Parse(parts[2]),
                        parts.Length == 4 ?
                        ushort.Parse(parts[3]) : (ushort)0
                    );
                    return result;
                }
                throw new Result<bool>("No version expression matched.");
            }
        }
    }
}
