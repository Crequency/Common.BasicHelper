using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicHelper.Util
{
    public struct Platform
    {

        /// <summary>
        /// 平台名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 平台版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 最小支持版本
        /// </summary>
        public Version MinistVersion { get; set; }
    }
}
