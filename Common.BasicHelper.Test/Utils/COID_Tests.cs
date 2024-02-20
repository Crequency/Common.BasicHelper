namespace Common.BasicHelper.Utils;

[TestClass]
public class COID_Tests
{
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Test_COID_Constructor()
    {
        _ = new COID("WFE-SD-WFE-VWE");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Test_COID_Part_Constructor()
    {
        _ = new COID("WFE-SD-WFE-VWE-SDF");
    }

    [TestMethod]
    public void Test_Equals()
    {
        var coid_a = COID_Helper.Build_COID("98KTD-N4GFV-J1RQK-E7CWJ-F9NTJ");
        var coid_b = COID_Helper.Build_COID("98KTD-N4GFV-J1RQK-E7CWJ-F9NTJ");

        Assert.AreEqual(coid_a, coid_b);
    }

    [TestMethod]
    public void Test_GetHashCode()
    {
        var coid_a = COID_Helper.Build_COID("98KTD-N4GFV-J1RQK-E7CWJ-F9NTJ");
        var coid_b = COID_Helper.Build_COID("98KTD-N4GFV-J1RQK-E7CWJ-F9NTJ");

        Assert.AreEqual(coid_a.GetHashCode(), coid_b.GetHashCode());
    }
}