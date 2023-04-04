namespace Common.BasicHelper.Utils.Extensions;

[TestClass]
public class StringHelperTests
{
    [TestMethod]
    public void Test_SeparateGroup()
    {
        var mac = "60F677F6C179";
        var formatedMac = mac.SeparateGroup(2, sb => sb.Append(':'));
        Assert.AreEqual(formatedMac, "60:F6:77:F6:C1:79");
    }

    [TestMethod]
    public void Test_IsNullOrEmpty()
    {
        string a = "asd";
        string b = "   ";
        string c = "";
        string? d = null;

        Assert.IsFalse(a.IsNullOrEmpty());
        Assert.IsFalse(b.IsNullOrEmpty());
        Assert.IsTrue(c.IsNullOrEmpty());
        Assert.IsTrue(d.IsNullOrEmpty());
    }

    [TestMethod]
    public void Test_IsNullOrWhiteSpace()
    {
        string a = "asd";
        string b = "   ";
        string c = "";
        string? d = null;

        Assert.IsFalse(a.IsNullOrWhiteSpace());
        Assert.IsTrue(b.IsNullOrWhiteSpace());
        Assert.IsTrue(c.IsNullOrWhiteSpace());
        Assert.IsTrue(d.IsNullOrWhiteSpace());
    }

    [TestMethod]
    public void Test_ExecuteAsCommand()
    {
        Console.WriteLine("help".ExecuteAsCommand());
    }

    [TestMethod]
    public async Task Test_ExecuteAsCommandAsync()
    {
        Console.WriteLine(await "help".ExecuteAsCommandAsync());
    }
}
