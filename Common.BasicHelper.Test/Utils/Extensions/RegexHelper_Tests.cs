using System.Text.RegularExpressions;
using Common.BasicHelper.Utils.Extensions;

namespace Common.BasicHelper.Test.Utils.Extensions;

[TestClass]
public partial class RegexHelper_Tests
{
    [TestMethod]
    public void Test_WhenSuccess()
    {
        _ = Regex_Test_WhenSuccess()
            .Match("Hello World")
            .WhenSuccess(x => Assert.AreEqual("Hello", x?.Groups[1].Value));
    }

    [GeneratedRegex("^(Hello).*$")]
    private static partial Regex Regex_Test_WhenSuccess();
}
