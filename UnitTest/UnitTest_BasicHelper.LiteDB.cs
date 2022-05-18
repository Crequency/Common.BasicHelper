using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BasicHelper.LiteDB;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class BasicHelper_LiteDB
    {
        [TestMethod]
        public void BasicFunctionTest()
        {
            DBManager manager = new();
            manager.CreateDataBase("Default");
            var db = (DataBase)manager.GetDataBase("Default").ReturnResult;
            db.AddTable("test1", new(new string[3]
            {
                "ID", "Name", "Class"
            }, new Type[3]
            {
                typeof(int), typeof(string), typeof(string)
            }));
            var test1 = (DataTable)db.GetTable("test1").ReturnResult;
            List<int> ids = new()
            {
                (int)test1.Add(new object[3]
                {
                    1, "白学  ", "日冕大班"
                }).ReturnResult,
                (int)test1.Add(new object[3]
                {
                    2, "岚依  ", "日冕大班"
                }).ReturnResult,
                (int)test1.Add(new object[3]
                {
                    3, "夏尔  ", "日冕向日葵班"
                }).ReturnResult,
                (int)test1.Add(new object[3]
                {
                    4, "伊欧", "日冕葵花子班"
                }).ReturnResult,
                (int)test1.Add(new object[3]
                {
                    5, "常青园晚", "日冕小小班"
                }).ReturnResult
            };
            test1.Update(4, "Name", "1V/1A");
            test1.Delete(5);
            foreach (string keys in test1.KeysProperty.Keys)
                Console.Write($"{keys}\t");
            Console.WriteLine();
            foreach (int id in ids)
            {
                if (test1.Exist(id))
                {
                    var result = ((manager
                        .GetDataBase("Default").ReturnResult as DataBase)
                        .GetTable("test1").ReturnResult as DataTable)
                        .Query(id).ReturnResult as List<object>;
                    foreach (var item in result)
                    {
                        Console.Write($"{item}\t");
                    }
                    Console.WriteLine();
                    Assert.AreEqual(id, result[0]);
                }
            }
        }

        [TestMethod]
        public void IOTest()
        {
            var manager = DBManager
                .GetTestDBManager(@"D:\Temp\Test\LiteDB")
                .ReturnResult as DBManager;

            manager.Save2File();
        }
    }
}
