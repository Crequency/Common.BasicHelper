namespace Common.BasicHelper.Utils;

[TestClass]
public class GUIDTests
{
    [TestMethod]
    public void Test_GenerateRandomGUIDPart()
    {
        for (int i = 0; i < 10; i++)
            Console.WriteLine(GUID_Helper.Random_GUID_Generate().GetString());
    }
}