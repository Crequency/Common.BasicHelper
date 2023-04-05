using System.Diagnostics;
using System.IO.Pipes;
using System.Net.Sockets;

namespace Common.BasicHelper.Utils.Extensions;

[TestClass]
public class CleanHelperTests
{
    [TestMethod]
    public void Test_CloseAndDispose()
    {
        // Arrange
        var memoryStream = new MemoryStream();
        var namedPipeServerStream = new NamedPipeServerStream("testpipe",
            PipeDirection.InOut);
        var binaryReader = new BinaryReader(memoryStream);
        var binaryWriter = new BinaryWriter(memoryStream);
        var streamReader = new StreamReader(memoryStream);
        var streamWriter = new StreamWriter(memoryStream);
        var process = new Process();
        var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

        // Act
        namedPipeServerStream.CloseAndDispose();
        binaryReader.CloseAndDispose();
        binaryWriter.CloseAndDispose();
        streamReader.CloseAndDispose();
        streamWriter.CloseAndDispose();
        process.CloseAndDispose();
        socket.CloseAndDispose();

        memoryStream.CloseAndDispose();

        // Assert
        Assert.IsTrue(namedPipeServerStream.IsConnected == false);
        Assert.IsTrue(binaryReader.BaseStream.CanRead == false);
        Assert.IsTrue(binaryWriter.BaseStream.CanWrite == false);
        Assert.IsTrue(streamReader.BaseStream.CanRead == false);
        Assert.IsTrue(streamWriter.BaseStream.CanWrite == false);
        Assert.IsTrue(socket.Connected == false);
    }
}