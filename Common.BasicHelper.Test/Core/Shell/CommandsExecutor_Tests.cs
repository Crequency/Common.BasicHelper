namespace Common.BasicHelper.Core.Shell;

[TestClass]
public class CommandsExecutor_Tests
{
    [TestMethod]
    public void Test_ExecuteAsCommand()
    {
        Console.WriteLine("help".ExecuteAsCommand());
    }

    [TestMethod]
    public async Task Test_ExecuteAsCommandAsync()
    {
        Console.WriteLine(await "help".ExecuteAsCommandAsync());
    }
}
