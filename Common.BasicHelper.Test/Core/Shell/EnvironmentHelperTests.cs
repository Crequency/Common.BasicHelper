using Common.BasicHelper.Core.Shell;

namespace Common.BasicHelper.Test.Core.Shell;

[TestClass]
public class EnvironmentHelperTests
{
    [TestMethod]
    public void Test_FindInPath()
    {
        var command = OperatingSystem.IsWindows() ? "help" : "cd";
        Assert.AreEqual(EnvironmentHelper.GetFilePathInPaths(command), command.FindInPath());
    }
}
