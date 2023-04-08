namespace Common.BasicHelper.Utils.Extensions;

[TestClass()]
public class ListHelper_Tests
{
    [TestMethod()]
    public void Test_ToCustomString()
    {
        Assert.AreEqual
        (
            new List<int>()
            {
                1, 2, 3, 4, 5, 6, 7,
            }.ToCustomString().Print(),
            "1,2,3,4,5,6,7"
        );

        Assert.AreEqual
        (
            new List<int>()
            {
                1, 2, 3, 4, 5, 6, 7,
            }.ToCustomString(cutEnding: false).Print(),
            "1,2,3,4,5,6,7,"
        );
    }
}
