namespace Common.BasicHelper.Net;

[TestClass]
public class BasicNetTests
{
    private const string testDownloadFilePath = "https://www.baidu.com/index.html";

    [TestMethod]
    public void Test_IsWebConected()
    {
        Assert.IsTrue(BasicNet.IsWebConected("localhost", 3));
    }

    [TestMethod]
    public void Test_DownloadFile()
    {
        var path = Path.GetFullPath($"{Path.GetTempPath()}/test_downloadFile.txt");

        BasicNet.DownloadFile(testDownloadFilePath, path);

        File.Delete(path);
    }

    [TestMethod]
    public void Test_WebDownloadFile()
    {
        var path = Path.GetFullPath($"{Path.GetTempPath()}/test_webDownloadFile.txt");

        BasicNet.WebDownloadFile(testDownloadFilePath, path);

        File.Delete(path);
    }
}