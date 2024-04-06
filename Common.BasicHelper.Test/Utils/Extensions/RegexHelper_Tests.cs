using System.Text.RegularExpressions;

namespace Common.BasicHelper.Utils.Extensions;

[TestClass]
public class RegexHelper_Tests
{
    [TestMethod]
    public void Test_WhenSuccess()
    {
#pragma warning disable SYSLIB1045 // 转换为“GeneratedRegexAttribute”。
        _ = Regex.Match("Hello World", "^(Hello).*$").WhenSuccess(
            x => Assert.AreEqual("Hello", x?.Groups[1].Value)
        );
#pragma warning restore SYSLIB1045 // 转换为“GeneratedRegexAttribute”。
    }
}
