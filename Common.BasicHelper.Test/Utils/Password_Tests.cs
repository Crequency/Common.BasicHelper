using Common.BasicHelper.Utils;

namespace Common.BasicHelper.Test.Utils;

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
    public void Test_GeneratePassword_1()
    {
        Assert.Throws<ArgumentException>(() =>
            Password.GeneratePassword(lengthRangeStart: null, lengthRangeEnd: null)
        );
    }

    [TestMethod]
    public void Test_GeneratePassword_2()
    {
        Assert.Throws<ArgumentException>(() =>
            Password.GeneratePassword(
                includeUppercase: false,
                includeLowercase: false,
                includeNumbers: false,
                includeSymbols: false
            )
        );
    }

    [TestMethod]
    public void Test_GeneratePassword_3()
    {
        Assert.Throws<ArgumentException>(() =>
            Password.GeneratePassword(
                supportedUppercases: "",
                supportedLowercases: "",
                supportedNumbers: "",
                supportedSymbols: ""
            )
        );
    }
}
