using Common.BasicHelper.Core.Shell;

namespace Common.BasicHelper.Test;

[TestClass]
public class CommadsExecutor_Test
{
    [TestMethod]
    public void TestCommand()
    {
        Console.WriteLine("help".ExecuteAsCommand());
    }
}
