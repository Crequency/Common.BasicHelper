using Common.BasicHelper.Utils.Extensions;

namespace Common.BasicHelper.Test.Utils.Extensions;

[TestClass]
public class Test_StringHelper
{
    [TestMethod]
    public void Test_SeparateGroup()
    {
        var mac = "60F677F6C179";
        var formatedMac = mac.SeparateGroup(2, sb => sb.Append(':'));
        Assert.AreEqual(formatedMac, "60:F6:77:F6:C1:79");
    }
}
