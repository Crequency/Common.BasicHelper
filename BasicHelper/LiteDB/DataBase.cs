using BasicHelper.Util;

namespace BasicHelper.LiteDB
{
    public class DataBase
    {
        private readonly Dictionary<string, DataTable> SubDataTables = new();

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
    }
}
