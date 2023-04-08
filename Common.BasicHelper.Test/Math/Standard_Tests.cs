using Common.BasicHelper.Utils.Extensions;

namespace Common.BasicHelper.Math;

[TestClass()]
public class Standard_Tests
{
    [TestMethod()]
    public void Test_GetPosition()
    {
        Standard.GetPosition(2468, 3).Print();

        2468.GetPosition(4).Print();
    }
}
