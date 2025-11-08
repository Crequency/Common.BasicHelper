using Common.BasicHelper.Math;
using Common.BasicHelper.Utils.Extensions;

namespace Common.BasicHelper.Test.Math;

[TestClass()]
public class Equation_Tests
{
    [TestMethod()]
    public void Test_SolveEquation()
    {
        Assert.AreEqual("5, 10", Equation.SolveEquation(3, 4, 55, 6, -2, 10).Print<double>());

        Assert.AreEqual(
            "2, 4, 6",
            Equation.SolveEquation(3, 5, -3, 8, 5, -1, 4, 30, 2, 3, -1, 10).Print<double>()
        );

        Assert.AreEqual(
            "5, 7, -7, 5",
            Equation
                .SolveEquation(2, 3, 5, 7, 31, 4, 5, 7, 11, 61, 3, 2, 4, 5, 26, 1, 1, 2, 3, 13)
                .Print<double>()
        );
    }
}
