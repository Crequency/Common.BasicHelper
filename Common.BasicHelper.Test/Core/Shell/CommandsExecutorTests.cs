namespace Common.BasicHelper.Core.Shell;

[TestClass]
public class CommandsExecutorTests
{
    [TestMethod]
    public void Test_Command()
    {
        Console.WriteLine("help".ExecuteAsCommand());
    }
}
