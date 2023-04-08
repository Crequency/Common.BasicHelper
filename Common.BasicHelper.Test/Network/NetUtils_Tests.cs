namespace Common.BasicHelper.Network;

[TestClass]
public class NetUtils_Tests
{
    private const string testDownloadFilePath = "https://www.baidu.com/index.html";

    [TestMethod]
    public void Test_IsWebConected()
    {
        Assert.IsTrue(NetUtils.IsWebConected("localhost", 3));
        Assert.IsFalse(NetUtils.IsWebConected("192.168.255.255", 3));
    }

    [TestMethod]
    public void Test_DownloadFile()
    {
        var path = Path.GetFullPath($"{Path.GetTempPath()}/test_downloadFile.txt");

        NetUtils.DownloadFile(testDownloadFilePath, path);

        File.Delete(path);
    }

    [TestMethod]
    public void Test_WebDownloadFile()
    {
        var path = Path.GetFullPath($"{Path.GetTempPath()}/test_webDownloadFile.txt");

        NetUtils.WebDownloadFile(testDownloadFilePath, path);

        File.Delete(path);
    }
}