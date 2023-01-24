using Common.BasicHelper.Util;

namespace Common.BasicHelper.Test;

[TestClass]
public class Util_Test
{
    [TestMethod]
    public void GUID_Part_Random_Generate_Test()
    {
        for (int i = 0; i < 10; i++)
            Console.WriteLine(GUID_Helper.Random_GUID_Generate().GetString());
    }
}