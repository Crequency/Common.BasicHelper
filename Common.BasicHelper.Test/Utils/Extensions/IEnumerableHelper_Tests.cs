namespace Common.BasicHelper.Utils.Extensions;

[TestClass()]
public class IEnumerableHelper_Tests
{
    [TestMethod()]
    public void Test_IndexOf()
    {
        var enumerable = new int[] { 2, 4, 3, 2, 5, 2, 6, 3 };

        var index = enumerable.IndexOf(x => x == 3);

        Assert.AreEqual(2, index);
    }

    [TestMethod()]
    public void Test_WhenCount()
    {
        var enumerable = new int[] { 2, 3, 5, 3, 2, 3, 4, 2, 56, 6, 4, 3, 453, 3, 3, 2 };

        enumerable.WhenCount(
            x => x == 4,
            i => i == 2,
            _ => Assert.IsNull(null)
        );
    }
}
