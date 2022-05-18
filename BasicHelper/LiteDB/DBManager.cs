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
        private string workbase = "./";

        /// <summary>
        /// 工作空间
        /// </summary>
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
            if (File.Exists($"{dirinfo.FullName}\\.LiteDB.config"))
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

        /// <summary>
        /// 构造函数
        /// </summary>
        public DBManager()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="workDir">工作空间路径</param>
        public DBManager(string workDir)
        {
            WorkBase = workDir;
        }

        private readonly Dictionary<string, DataBase> DataBases = new();

        /// <summary>
        /// 创建新数据库
        /// </summary>
        /// <param name="name">数据库名称</param>
        /// <returns>数据库ID</returns>
        /// <exception cref="Result{bool}">已经存在此数据库异常</exception>
        public void CreateDataBase(string name)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(name, RegexStrings.LiteDB_Name_Limit))
                throw new Result<bool>("Illegal name.");
            if (DataBases.ContainsKey(name))
                throw new Result<bool>("This database already existed.");
            DataBases.Add(name, new());
        }

        /// <summary>
        /// 获取某数据库实例
        /// </summary>
        /// <param name="name">数据库名称</param>
        /// <returns>数据库</returns>
        /// <exception cref="Result{bool}">不存在此数据库异常</exception>
        public Result<DataBase> GetDataBase(string name)
        {
            if (DataBases.ContainsKey(name))
                return new(DataBases[name]);
            throw new Result<bool>("No this database.");
        }

        /// <summary>
        /// 移除某数据库
        /// </summary>
        /// <param name="name">数据库名称</param>
        /// <exception cref="Result{bool}">不存在此数据库异常</exception>
        public void RemoveDataBase(string name)
        {
            if (DataBases.ContainsKey(name))
                DataBases.Remove(name);
            throw new Result<bool>("No this database.");
        }

        /// <summary>
        /// 保存数据库到本地
        /// </summary>
        /// <exception cref="Result{bool}">初始化失败异常</exception>
        public void Save2File()
        {
            try
            {
                IO.FileHelper.DeleteFolder(WorkBase);
                Directory.CreateDirectory(Path.GetFullPath(WorkBase));
            }
            catch(Exception o)
            {
                throw new Result<bool>($"Directory init failed cause: \n{o.Message}");
            }

            foreach (var item in DataBases)
                item.Value.Save2File(item.Key, WorkBase);
        }

        /// <summary>
        /// 获取一个测试数据库
        /// </summary>
        /// <param name="workbase">工作空间</param>
        /// <returns>数据库实例</returns>
        public Result<DBManager> GetTestDBManager(string workbase)
        {
            var dbManager = new DBManager(workbase);
            dbManager.CreateDataBase("Test");
            var db_test = dbManager.GetDataBase("Test").ReturnResult as DataBase;
            db_test.AddTable("Test_DT", new(new string[]
            {
                "ID", "Title", "URL", "DESCR"
            }, new Type[]{
                typeof(int), typeof(string), typeof(string), typeof(string)
            }));
            var dt_test = db_test.GetTable("Test_DT").ReturnResult as DataTable;
            var ids = new List<int>()
            {
                (int)dt_test.Add(new object[]
                {
                    1, "BasicHelper", "https://github.com/Catrol-org/BasicHelper", "项目主页"
                }).ReturnResult,
                (int)dt_test.Add(new object[]
                {
                    2, "LiteDB", "https://github.com/Catrol-org/BasicHelper/LiteDB",
                    "轻量化的本地数据库, 适合客户端配置系统"
                }).ReturnResult,
                (int)dt_test.Add(new object[]
                {
                    3, "常青园晚", "https://blog.catrol.cn", "主要作者博客"
                }).ReturnResult
            };
            return new(dbManager);
        }
    }
}
