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
}
