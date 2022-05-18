using BasicHelper.Util;
using System.Runtime.Serialization.Formatters.Binary;

namespace BasicHelper.LiteDB
{
    public class DataBase
    {
        private readonly Dictionary<string, DataTable> SubDataTables = new();

        /// <summary>
        /// 数据表属性
        /// </summary>
        public Dictionary<string, DataTable> SubDataTablesProperty { get => SubDataTables; }

        /// <summary>
        /// 新增数据表
        /// </summary>
        /// <param name="name">表名称</param>
        /// <param name="dt">表</param>
        /// <exception cref="Result{bool}">已经存在此表</exception>
        public void AddTable(string name, DataTable dt)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(name, RegexStrings.LiteDB_Name_Limit))
                throw new Result<bool>("Illegal name.");
            if (SubDataTables.ContainsKey(name))
                throw new Result<bool>("This data table already exists.");
            else SubDataTables.Add(name, dt);
        }

        /// <summary>
        /// 通过表名获取表
        /// </summary>
        /// <param name="name">表名</param>
        /// <returns>表</returns>
        /// <exception cref="Result{bool}">不存在此表</exception>
        public Result<DataTable> GetTable(string name)
        {
            if (SubDataTables.ContainsKey(name))
                return new(SubDataTables[name]);
            else throw new Result<bool>("This data table didn't exists.");
        }

        /// <summary>
        /// 通过表名移除表
        /// </summary>
        /// <param name="name">表名</param>
        /// <exception cref="Result{bool}">此表不存在</exception>
        public void RemoveTable(string name)
        {
            if (SubDataTables.ContainsKey(name))
                SubDataTables.Remove(name);
            else throw new Result<bool>("This data table didn't exists.");
        }

        /// <summary>
        /// 通知每个数据表写入本地
        /// </summary>
        /// <param name="name">数据库名称</param>
        /// <param name="path">文件夹路径</param>
        public void Save2File(string name, string path)
        {
            foreach (var item in SubDataTables)
            {
                FileStream stream = new($"{path}\\{name}.{item.Key}.dat", FileMode.Create, FileAccess.Write);
                BinaryFormatter formatter = new();
                formatter.Serialize(stream, item.Value);
                stream.Close();
            }
        }

        /// <summary>
        /// 从文件恢复数据表
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>数据表</returns>
        public static Result<DataTable> Recovery(string filename)
        {
            Console.WriteLine(filename);
            FileStream stream = new(filename, FileMode.Open, FileAccess.Read);
            return new(new BinaryFormatter().Deserialize(stream) as DataTable);
        }
    }
}
