using System.Net.NetworkInformation;
using Common.BasicHelper.Utils.Extensions;

namespace Common.BasicHelper.Test.Utils.Extensions;

[TestClass()]
public class Dumpper_Tests
{
    [TestMethod()]
    public void Test_Dump()
    {
        new Queue<int?>().Push(null).Push(1).Dump();

        if (OperatingSystem.IsWindows())
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var iface in interfaces)
                iface.Dump();
        }
    }

    [TestMethod()]
    public void Test_Dump2Lines()
    {
        new Queue<int?>().Push(null).Push(1).Dump2Lines();

        if (OperatingSystem.IsWindows())
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var iface in interfaces)
                iface.Dump2Lines();
        }
    }

    [TestMethod()]
    public void Test_Print()
    {
        Assert.AreEqual("Test", "Test".Print());

        Assert.AreEqual("24523", 24523.Print());

        Assert.AreEqual("1, 2, 3", new List<int>() { 1, 2, 3 }.Print<int>());

        var temp = new string[3] { "12", "34", "56" };

        Assert.AreEqual(
            """
            12
            34
            56

            """,
            temp.Print<string>()
        );
    }
}
