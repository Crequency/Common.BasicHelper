using Common.BasicHelper.Utils;

namespace Common.BasicHelper.Test.Utils;

[TestClass]
public class COID_Helper_Tests
{
    [TestMethod]
    public void Test_Random_COID_Generate()
    {
        for (int i = 0; i < 10; ++i)
            Console.WriteLine(COID_Helper.Random_COID_Generate().GetString());
    }

    [TestMethod]
    public void Test_Build_COID()
    {
        _ = COID_Helper.Build_COID("98KTD-N4GFV-J1RQK-E7CWJ-F9NTJ");

        Assert.Throws<ArgumentException>(() => COID_Helper.Build_COID("98KTD-N4GFV-J1RQK-E7CWJ"));
    }

    [TestMethod]
    public void Test_Build_COID_Part()
    {
        var coid_part = COID_Helper.Build_COID_Part("98KTD");

        Assert.AreEqual("98KTD", coid_part.ToString());

        coid_part = new COID_Part()
        {
            A = 'A',
            B = 'B',
            C = 'C',
            D = 'D',
            E = 'E',
        };

        Assert.AreEqual("ABCDE", coid_part.ToString());

        Assert.Throws<ArgumentException>(() => COID_Helper.Build_COID_Part("1234"));

        Assert.Throws<FormatException>(() => COID_Helper.Build_COID_Part("1#5$9"));
    }
}
