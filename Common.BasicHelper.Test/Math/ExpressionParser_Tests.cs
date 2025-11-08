using System.Diagnostics;
using Common.BasicHelper.Math;

namespace Common.BasicHelper.Test.Math;

[TestClass()]
public class ExpressionParser_Tests
{
    [TestMethod()]
    public void GeneralTest()
    {
        var exprStr = "39 / 3 + 16 * 5 - (24 * 24 + 5 + 7)";

        Debug.WriteLine($"Given Expression: {exprStr}");

        var parser = new ExpressionParser();
        var expression = parser.Parse(exprStr);

        Debug.WriteLine($"Expression Tree to String: {expression}");
        Debug.WriteLine($"Expression Tree Result: {expression.Result}");
    }
}
