namespace Common.BasicHelper.Math;

[TestClass()]
public class Tricks_Tests
{
    [TestMethod()]
    public void Test_GetMultiplicationTable()
    {
        Console.WriteLine(Tricks.GetMultiplicationTable(1, 9));

        Console.WriteLine(Tricks.GetMultiplicationTable(9, 1, false));

        Console.WriteLine(1.GetMultiplicationTableTo(9));

        Console.WriteLine(9.GetMultiplicationTableTo(1, false));
    }
}
