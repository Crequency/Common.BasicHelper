using System.Net.NetworkInformation;

namespace Common.BasicHelper.Utils.Extensions;

[TestClass()]
public class Dumpper_Tests
{
    [TestMethod()]
    public void Test_Dump()
    {
        new Queue<int?>()
            .Push(null)
            .Push(1)
            .Dump()
            ;

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
        new Queue<int?>()
            .Push(null)
            .Push(1)
            .Dump2Lines()
            ;

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
        Assert.AreEqual("Test".Print(), "Test");

        Assert.AreEqual(24523.Print(), "24523");

        Assert.AreEqual(new List<int>()
        {
            1, 2, 3
        }.Print<int>(), "1, 2, 3");

        Assert.AreEqual(new string[3]
        {
            "12",
            "34",
            "56"
        }.Print<string>(), """
        12
        34
        56
        
        """);
    }
}
