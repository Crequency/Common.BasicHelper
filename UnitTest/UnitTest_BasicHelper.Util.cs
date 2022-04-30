using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BasicHelper.Util;

namespace UnitTest
{
    [TestClass]
    public class BasicHelper_Util
    {
        [TestMethod]
        public void GUID_Part_Random_Generate_Test()
        {
            for (int i = 0; i < 10; i++)
                Console.WriteLine(GUID_Helper.Random_GUID_Generate().GetString());
        }
    }
}