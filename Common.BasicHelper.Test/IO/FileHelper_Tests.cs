using System.Text;
using Common.BasicHelper.Utils.Extensions;

namespace Common.BasicHelper.IO;

[TestClass()]
public class FileHelper_Tests
{
    [TestMethod()]
    public void Test_WriteIn()
    {
        var file = Path.GetTempFileName();

        file.Print();

        FileHelper.WriteIn(file, "Test");

        var read = File.ReadAllText(file);

        File.Delete(file);

        Assert.AreEqual(read, "Test");
    }

    [TestMethod()]
    public void Test_Append()
    {
        var file = Path.GetTempFileName();

        file.Print();

        FileHelper.WriteIn(file, "Test");

        FileHelper.Append(file, "Test");

        var read = File.ReadAllText(file);

        File.Delete(file);

        Assert.AreEqual(read, $"Test{Environment.NewLine}Test");
    }

    [TestMethod()]
    public void Test_WriteBytesTo()
    {
        var file = Path.GetTempFileName();

        file.Print();

        FileHelper.WriteBytesTo(file, Encoding.UTF8.GetBytes("Test"));

        var read = File.ReadAllText(file);

        File.Delete(file);

        Assert.AreEqual(read, "Test");
    }

    [TestMethod()]
    public void Test_WriteBytesToFile()
    {
        var file = Path.GetTempFileName();

        file.Print();

        FileHelper.WriteBytesToFile(file, Encoding.UTF8.GetBytes("Test"));

        var read = File.ReadAllText(file);

        File.Delete(file);

        Assert.AreEqual(read, "Test");
    }

    [TestMethod()]
    public void Test_ReadAll()
    {
        var file = Path.GetTempFileName();

        file.Print();

        FileHelper.WriteIn(file, "Test");

        var read = FileHelper.ReadAll(file);

        File.Delete(file);

        Assert.AreEqual(read, "Test");
    }

    [TestMethod()]
    public async Task Test_ReadAllAsync()
    {
        var file = Path.GetTempFileName();

        file.Print();

        FileHelper.WriteIn(file, "Test");

        var read = await FileHelper.ReadAllAsync(file);

        File.Delete(file);

        Assert.AreEqual(read, "Test");
    }

    [TestMethod()]
    public void Test_ReadAllBytes()
    {
        var file = Path.GetTempFileName();

        file.Print();

        var bytes = Encoding.UTF8.GetBytes("Test");

        FileHelper.WriteBytesTo(file, bytes);

        var read = FileHelper.ReadAllBytes(file);

        File.Delete(file);

        Assert.AreEqual(read.LongLength, bytes.LongLength);

        for (int i = 0; i < read.LongLength; ++i)
            Assert.AreEqual(read[i], bytes[i]);
    }

    [TestMethod()]
    public void Test_FileToByte()
    {
        var file = Path.GetTempFileName();

        file.Print();

        var bytes = Encoding.UTF8.GetBytes("Test");

        FileHelper.WriteBytesTo(file, bytes);

        var read = FileHelper.FileToBytes(file);

        File.Delete(file);

        Assert.AreEqual(read.LongLength, bytes.LongLength);

        for (int i = 0; i < read.LongLength; ++i)
            Assert.AreEqual(read[i], bytes[i]);
    }

    [TestMethod()]
    public void Test_ByteToFile()
    {
        var file = Path.GetTempFileName();

        file.Print();

        var bytes = Encoding.UTF8.GetBytes("Test");

        FileHelper.ByteToFile(bytes, file);

        var read = FileHelper.ReadAllBytes(file);

        File.Delete(file);

        Assert.AreEqual(read.LongLength, bytes.LongLength);

        for (int i = 0; i < read.LongLength; ++i)
            Assert.AreEqual(read[i], bytes[i]);
    }

    [TestMethod()]
    public void Test_CreateFile()
    {
        var file = Path.GetTempFileName();

        file.Print();

        var bytes = Encoding.UTF8.GetBytes("Test");

        FileHelper.CreateFile(bytes, file);

        var read = FileHelper.ReadAllBytes(file);

        File.Delete(file);

        Assert.AreEqual(read.LongLength, bytes.LongLength);

        for (int i = 0; i < read.LongLength; ++i)
            Assert.AreEqual(read[i], bytes[i]);
    }
}
