using System.Diagnostics;
using System.IO.Pipes;
using System.Net.Sockets;
using Common.BasicHelper.Utils.Extensions;

namespace Common.BasicHelper.Test.Utils.Extensions;

[TestClass]
public class CleanHelper_Tests
{
    [TestMethod]
    public void Test_CloseAndDispose()
    {
        // Arrange
        var memoryStream = new MemoryStream();
        var namedPipeServerStream = new NamedPipeServerStream("testpipe", PipeDirection.InOut);
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
        Assert.IsFalse(namedPipeServerStream.IsConnected);
        Assert.IsFalse(binaryReader.BaseStream.CanRead);
        Assert.IsFalse(binaryWriter.BaseStream.CanWrite);
        Assert.IsFalse(streamReader.BaseStream.CanRead);
        Assert.IsFalse(streamWriter.BaseStream.CanWrite);
        Assert.IsFalse(socket.Connected);
    }
}
