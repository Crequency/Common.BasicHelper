using Common.BasicHelper.Utils;

namespace Common.BasicHelper.Test.Utils;

[TestClass]
public class Test_GUID
{
    [TestMethod]
    public void Test_GenerateRandomGUIDPart()
    {
        for (int i = 0; i < 10; i++)
            Console.WriteLine(GUID_Helper.Random_GUID_Generate().GetString());
    }
}