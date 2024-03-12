namespace Common.BasicHelper.Utils;

[TestClass]
public class Password_Tests
{
    [TestMethod]
    public void Test_GeneratePassword()
    {
        foreach (var _ in Enumerable.Range(0, 10))
            Console.WriteLine(Password.GeneratePassword(length: 12));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Test_GeneratePassword_1()
    {
        Password.GeneratePassword(lengthRangeStart: null, lengthRangeEnd: null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Test_GeneratePassword_2()
    {
        Password.GeneratePassword(
            includeUppercase: false,
            includeLowercase: false,
            includeNumbers: false,
            includeSymbols: false
        );
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Test_GeneratePassword_3()
    {
        Password.GeneratePassword(
            supportedUppercases: "",
            supportedLowercases: "",
            supportedNumbers: "",
            supportedSymbols: ""
        );
    }
}
