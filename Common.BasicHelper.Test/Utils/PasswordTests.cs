namespace Common.BasicHelper.Utils;

[TestClass]
public class PasswordTests
{
    [TestMethod]
    public void Test_GeneratePassword()
    {
        foreach (var item in Enumerable.Range(0, 10))
            Console.WriteLine(Password.GeneratePassword(length: 12));
    }
}
