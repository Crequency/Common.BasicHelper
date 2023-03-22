using Common.BasicHelper.Core.Shell;

namespace Common.BasicHelper.Test.Core.Shell;

[TestClass]
public class Test_CommandsExecutor
{
    [TestMethod]
    public void Test_Command()
    {
        Console.WriteLine("help".ExecuteAsCommand());
    }
}
