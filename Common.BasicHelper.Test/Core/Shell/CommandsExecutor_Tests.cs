namespace Common.BasicHelper.Core.Shell;

[TestClass]
public class CommandsExecutor_Tests
{
    [TestMethod]
    public void Test_ExecuteAsCommand()
    {
        if (OperatingSystem.IsWindows())
            Console.WriteLine("help".ExecuteAsCommand());

        if (OperatingSystem.IsLinux())
            Console.WriteLine("apt".ExecuteAsCommand("-v"));

        if (OperatingSystem.IsMacOS())
            Console.WriteLine("sw_vers".ExecuteAsCommand());
    }

    [TestMethod]
    public async Task Test_ExecuteAsCommandAsync()
    {
        if (OperatingSystem.IsWindows())
            Console.WriteLine(await "help".ExecuteAsCommandAsync());

        if (OperatingSystem.IsLinux())
            Console.WriteLine(await "apt".ExecuteAsCommandAsync("-v"));

        if (OperatingSystem.IsMacOS())
            Console.WriteLine(await "sw_vers".ExecuteAsCommandAsync());
    }
}
