namespace Common.BasicHelper.Math;

[TestClass()]
public class Expression_Tests
{
    [TestMethod()]
    public void Test_Expression()
    {
        var expr = (39 / 3) + (16 * 5) - ((24 * 24) + 5 + 7);

        var tree = new Expression()
        {
            Type = CalculationType.Add,
            Left = new()
            {
                Type = CalculationType.Division,
                Left = Expression.FromValue(39),
                Right = Expression.FromValue(3)
            },
            Right = new()
            {
                Type = CalculationType.Substraction,
                Left = new()
                {
                    Type = CalculationType.Multiply,
                    Left = Expression.FromValue(16),
                    Right = Expression.FromValue(5),
                },
                Right = new()
                {
                    Type = CalculationType.Add,
                    Left = new()
                    {
                        Type = CalculationType.Power,
                        Left = Expression.FromValue(24),
                        Right = Expression.FromValue(2)
                    },
                    Right = new()
                    {
                        Type = CalculationType.Add,
                        Left = Expression.FromValue(5),
                        Right = Expression.FromValue(7)
                    }
                }
            }
        };

        Assert.AreEqual(expr, tree.Result);
    }
}
