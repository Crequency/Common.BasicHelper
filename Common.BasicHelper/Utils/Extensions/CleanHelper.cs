using System;
using System.IO;
using System.Net;

namespace Common.BasicHelper.Util.Extension;

public static class CleanHelper
{
    /// <summary>
    /// 关闭并释放对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="obj">对象</param>
    public static void CloseAndDispose<T>(this T obj) where T : IDisposable
    {
        if (obj is Stream) (obj as Stream).Close();

        else if (obj is BinaryReader) (obj as BinaryReader).Close();

        else if (obj is BinaryWriter) (obj as BinaryWriter).Close();

        else if (obj is TextReader) (obj as TextReader).Close();

        else if (obj is TextWriter) (obj as TextWriter).Close();

        else if (obj is WebResponse) (obj as WebResponse).Close();

        obj.Dispose();
    }
}
