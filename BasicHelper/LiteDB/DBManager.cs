using BasicHelper.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicHelper.LiteDB
{
    public class DBManager
    {
        /// <summary>
        /// 工作空间
        /// </summary>
        private string workbase = "./";

        public string WorkBase
        {
            get { return workbase; }
            set
            {
                workbase = value;
                CheckDirectory();
            }
        }

        /// <summary>
        /// 检查工作目录合法性, 并初始化数据树
        /// </summary>
        /// <exception cref="Result{bool}">非空目录异常</exception>
        private void CheckDirectory()
        {
            DirectoryInfo dirinfo = new(WorkBase);
            if (File.Exists($"{dirinfo.FullName}\\LiteDB.config"))
                Init();
            if (
                !dirinfo.Exists ||
                dirinfo.GetFiles().Length >= 1 ||
                dirinfo.GetDirectories().Length >= 1
                )
                throw new Result<bool>("New workbase need a empty directory.");
        }

        /// <summary>
        /// 从现有数据库中初始化
        /// </summary>
        private void Init()
        {

        }

        public DBManager(string workDir)
        {
            WorkBase = workDir;
        }

    }
}
