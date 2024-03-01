using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Net;
using System.Net.Sockets;

namespace Common.BasicHelper.Utils.Extensions;

public static class CleanHelper
{
    /// <summary>
    /// 关闭并释放对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="obj">对象</param>
    /// <param name="skipClose">是否跳过关闭</param>
    public static void CloseAndDispose<T>(this T obj, bool skipClose = false) where T : IDisposable
    {

        if (skipClose) { }

        else if (obj is Stream stream) stream?.Close();

        // Below types based on `Stream`
        // 
        // - FileStream
        // - MemoryStream
        // - CryptoStream
        // - NetworkStream
        // - DeflateStream
        // - GZipStream

        else if (obj is PipeStream pipeStream) pipeStream?.Close();

        // Below types based on `PipeStream`:
        // 
        // - NamedPipeClientStream
        // - NamedPipeServerStream

        else if (obj is BinaryReader binaryReader) binaryReader?.Close();

        else if (obj is BinaryWriter binaryWriter) binaryWriter?.Close();

        else if (obj is TextReader textReader) textReader?.Close();

        else if (obj is TextWriter textWriter) textWriter?.Close();

        // Below types based on `TextReader` or `TextWriter`
        // 
        // - StreamReader
        // - StreamWriter

        else if (obj is WebResponse webResponse) webResponse?.Close();

        else if (obj is Process process) process?.Close();

        else if (obj is Socket socket) socket?.Close();

        obj.Dispose();
    }
}
