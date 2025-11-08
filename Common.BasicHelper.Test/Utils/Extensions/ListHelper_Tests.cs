using Common.BasicHelper.Utils.Extensions;

namespace Common.BasicHelper.Test.Utils.Extensions;

[TestClass()]
public class ListHelper_Tests
{
    [TestMethod()]
    public void Test_ToCustomString()
    {
        Assert.AreEqual(
            "1,2,3,4,5,6,7",
            new List<int>() { 1, 2, 3, 4, 5, 6, 7 }
                .ToCustomString()
                .Print()
        );

        Assert.AreEqual(
            "1,2,3,4,5,6,7,",
            new List<int>() { 1, 2, 3, 4, 5, 6, 7 }
                .ToCustomString(cutEnding: false)
                .Print()
        );
    }
}
