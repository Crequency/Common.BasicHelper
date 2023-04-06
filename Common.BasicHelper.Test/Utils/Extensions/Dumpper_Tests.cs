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
            .Print()
            ;

        var interfaces = NetworkInterface.GetAllNetworkInterfaces();
        foreach (var iface in interfaces)
            iface.Dump().Print();
    }

    [TestMethod()]
    public void Test_Dump2Lines()
    {
        new Queue<int?>()
            .Push(null)
            .Push(1)
            .Dump2Lines()
            .Print()
            ;

        var interfaces = NetworkInterface.GetAllNetworkInterfaces();
        foreach (var iface in interfaces)
            iface.Dump2Lines().Print();
    }

    [TestMethod()]
    public void Test_Print()
    {
        "Test".Print();

        24523.Print();
    }
}
