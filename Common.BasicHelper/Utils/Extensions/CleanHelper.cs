using System;
using System.IO;
using System.Net;

namespace Common.BasicHelper.Utils.Extensions;

public static class CleanHelper
{
    /// <summary>
    /// 关闭并释放对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="obj">对象</param>
    public static void CloseAndDispose<T>(this T obj) where T : IDisposable
    {
        if (false) { }

        else if (obj is Stream stream) stream?.Close();

        else if (obj is BinaryReader binaryReader) binaryReader?.Close();

        else if (obj is BinaryWriter binaryWriter) binaryWriter?.Close();

        else if (obj is TextReader textReader) textReader?.Close();

        else if (obj is TextWriter textWriter) textWriter?.Close();

        else if (obj is WebResponse webResponse) webResponse?.Close();

        obj.Dispose();
    }
}
