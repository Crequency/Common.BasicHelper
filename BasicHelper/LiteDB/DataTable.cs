using BasicHelper.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicHelper.LiteDB
{
    public class DataTable
    {
        /// <summary>
        /// 数据表表头结构名称和限定数据类型
        /// </summary>
        private readonly Dictionary<string, Type> Keys = new();

        /// <summary>
        /// 数据表表头结构限定数据类型
        /// </summary>
        private readonly List<Type> KeyTypes = new();

        /// <summary>
        /// 数据表内容列表
        /// </summary>
        private readonly Dictionary<int, List<object>> Values = new();

        /// <summary>
        /// 最大应赋予的ID
        /// </summary>
        private int maxid = 0;

        private int MaxID
        {
            get
            {
                ++maxid;
                return maxid;
            }
        }

        /// <summary>
        /// 被删除的记录所使用的ID列表
        /// </summary>
        private readonly List<int> DeletedIDs = new();

        /// <summary>
        /// 获取下一个应该使用的ID
        /// </summary>
        public int NextID
        {
            get
            {
                if (DeletedIDs.Count == 0)
                    return MaxID;
                else
                {
                    int id = DeletedIDs[0];
                    DeletedIDs.RemoveAt(0);
                    return id;
                }
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="names">表头结构列名称</param>
        /// <param name="types">表头结构列类型</param>
        /// <exception cref="Result{bool}">初始化失败异常</exception>
        public DataTable(string[] names, Type[] types)
        {
            if (names.Length != types.Length)
                throw new Result<bool>("Invalid input arguments for different length.");
            else
            {
                for (int i = 0; i < names.Length; i++)
                {
                    Keys.Add(names[i], types[i]);
                    KeyTypes.Add(types[i]);
                }
            }
        }

        /// <summary>
        /// 新增一个记录
        /// </summary>
        /// <param name="values">值列表, 需与数据表表头类型对应</param>
        /// <returns>该记录的ID</returns>
        /// <exception cref="Result{bool}">引发新增失败的异常</exception>
        public Result<int> Add(object[] values)
        {
            if (values.Length != Keys.Count)
                throw new Result<bool>("Input values' length is less length than std keys.");
            var item = new List<object>();
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] != null)
                    if (values[i].GetType() == KeyTypes[i])
                        item.Add(values[i]);
                    else throw new Result<bool>($"Error type for item[{i}] " +
                        $"which is {values[i].GetType()} " +
                        $"but need {KeyTypes[i]}.");
                else throw new Result<bool>($"Item[{i}] is `null`.");
            }
            int id = NextID;
            Values.Add(id, item);
            return new(id);
        }

        /// <summary>
        /// 通过ID查询记录
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>记录</returns>
        public Result<List<object>> Query(int id)
        {
            if (Values.TryGetValue(id, out List<object>? result))
                return new(result);
            else throw new Result<bool>("No this record.");
        }

        /// <summary>
        /// 通过ID删除一条记录
        /// </summary>
        /// <param name="id">ID</param>
        public void Delete(int id)
        {
            if (Values.ContainsKey(id))
            {
                Values.Remove(id);
                DeletedIDs.Add(id);
            }
            else throw new Result<bool>($"Didn't exist key -> {id}.");
        }

        /// <summary>
        /// 通过ID更新一个记录
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="col">要修改的列</param>
        /// <param name="value">新的值</param>
        public void Update(int id, string col, object value)
        {
            if (value.GetType() != Keys[col])
                throw new Result<bool>($"Type of -> {value} not match {Keys[col]}.");
            
            //TODO: 通过列名找到单元格并更新它的值
        }
    }
}
