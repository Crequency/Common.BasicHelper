namespace Common.BasicHelper.Math;

[TestClass()]
public class Calculator_Tests
{
    [TestMethod()]
    public void Test_Calculate()
    {
        // 15 = 5 + 10
        Assert.AreEqual(
            15,
            Calculator.Calculate(
                Expression.FromValue(5),
                Expression.FromValue(10),
                CalculationType.Add
            )
        );

        // 5 = 10 - 5
        Assert.AreEqual(
            5,
            Calculator.Calculate(
                Expression.FromValue(10),
                Expression.FromValue(5),
                CalculationType.Substraction
            )
        );

        // 50 = 5 * 10
        Assert.AreEqual(
            50,
            Calculator.Calculate(
                Expression.FromValue(5),
                Expression.FromValue(10),
                CalculationType.Multiply
            )
        );

        // 0.5 = 5 / 10
        Assert.AreEqual(
            0.5,
            Calculator.Calculate(
                Expression.FromValue(5),
                Expression.FromValue(10),
                CalculationType.Division
            )
        );

        // 1024 = 2 ^ 10
        Assert.AreEqual(
            1024,
            Calculator.Calculate(
                Expression.FromValue(2),
                Expression.FromValue(10),
                CalculationType.Power
            )
        );
    }
}
