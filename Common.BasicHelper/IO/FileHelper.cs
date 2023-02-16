using Common.BasicHelper.Util;
using Common.BasicHelper.Util.Extension;
using System;
using System.IO;
using System.Threading.Tasks;

#pragma warning disable IDE0051 // 删除未使用的私有成员

namespace Common.BasicHelper.IO;

public class FileHelper
{
    /// <summary>
    /// 向指定的路径文件写入内容
    /// </summary>
    /// <param name="path">指定的路径</param>
    /// <param name="content">内容</param>
    /// <returns>写入是否成功以及异常信息</returns>
    public static Result<bool> WriteIn(string path, string content)
    {
        try
        {
            if (File.Exists(path)) File.Delete(path);

            File.Create(path).Close();

            var fs = new FileStream(path, FileMode.Open);
            var sw = new StreamWriter(fs);

            sw.Write(content);
            sw.Flush();

            sw.CloseAndDispose();
            fs.CloseAndDispose();

            return new Result<bool>(true);
        }
        catch (Exception o)
        {
            throw new Result<bool>(o.Message);
        }
    }

    /// <summary>
    /// 向指定路径追加文本，如果路径不存在，则创造该路径
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="content">要追加的内容</param>
    public static void Append(string path, string content)
        => WriteIn(path, $"{ReadAll(path)}\n{content}");

    /// <summary>
    /// 以二进制流写入指定路径全部内容
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="content">内容</param>
    /// <returns>异常信息</returns>
    public static Result<bool> WriteByteIn(string path, byte[] content)
    {
        try
        {
            if (!File.Exists(path))
                File.Create(path);

            var fs = new FileStream(path, FileMode.Open);
            var bw = new BinaryWriter(fs);

            bw.Write(content);
            bw.Flush();

            bw.CloseAndDispose();
            fs.CloseAndDispose();

            return new Result<bool>(true);
        }
        catch (Exception p)
        {
            throw new Result<bool>(p.Message);
        }
    }

    /// <summary>
    /// 以二进制流写入指定路径全部内容
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="content">内容</param>
    public static void WriteBytesToFile(string path, byte[] content)
    {
        var fs_write = new FileStream(path, FileMode.Open);

        fs_write.Write(content, 0, content.Length);

        fs_write.CloseAndDispose();
    }

    /// <summary>
    /// 读取指定路径的全部内容
    /// </summary>
    /// <param name="path">指定路径</param>
    /// <returns>内容或异常信息</returns>
    public static string ReadAll(string path)
    {
        if (File.Exists(path))
        {
            var fs = new FileStream(path, FileMode.Open);
            var sr = new StreamReader(fs);

            var content = sr.ReadToEnd();

            sr.CloseAndDispose();
            fs.CloseAndDispose();

            return content;
        }
        else throw new Result<bool>("File didn't exists.");
    }

    /// <summary>
    /// 异步读取指定路径的全部内容
    /// </summary>
    /// <param name="path">指定路径</param>
    /// <returns>内容或异常信息</returns>
    /// <exception cref="Result{bool}">异常</exception>
    public static async Task<string> ReadAllAsync(string path)
    {
        try
        {
            var fs = new FileStream(path, FileMode.Open);
            var sr = new StreamReader(fs);

            var result = await sr.ReadToEndAsync();

            sr.CloseAndDispose();
            fs.CloseAndDispose();

            return result;
        }
        catch (Exception e)
        {
            throw new Result<bool>(e.Message);
        }
    }

    /// <summary>
    /// 以二进制流读取指定路径全部内容
    /// </summary>
    /// <param name="path">路径</param>
    /// <returns>二进制流</returns>
    public static byte[] ReadByteAll(string path)
    {
        var fs = new FileStream(path, FileMode.Open);
        var br = new BinaryReader(fs);

        byte[] byData = br.ReadBytes((int)fs.Length);

        br.CloseAndDispose();
        fs.CloseAndDispose();

        return byData;
    }

    /// <summary>
    /// 二进制流读取文件
    /// </summary>
    /// <param name="filePath">文件路径</param>
    /// <returns>二进制流</returns>
    private static byte[] FileToBytes(string filePath)
    {
        var fi = new FileInfo(filePath);
        var buffer = new byte[fi.Length];

        var fs = fi.OpenRead();

        fs.Read(buffer, 0, Convert.ToInt32(fi.Length));

        fs.CloseAndDispose();

        return buffer;
    }

    /// <summary>
    /// 二进制流创建文件
    /// 如果文件存在，则覆盖原文件
    /// </summary>
    /// <param name="fileBuffer">二进制流</param>
    /// <param name="newFilePath">文件路径</param>
    private static void CreateFile(byte[] fileBuffer, string newFilePath)
    {
        if (File.Exists(newFilePath))
            File.Delete(newFilePath);

        var fs = new FileStream(newFilePath, FileMode.CreateNew);
        var bw = new BinaryWriter(fs);

        bw.Write(fileBuffer, 0, fileBuffer.Length); // 用文件流生成一个文件

        bw.CloseAndDispose();
        fs.CloseAndDispose();
    }

    /// <summary>
    /// 递归删除目录下所有文件夹/文件包括子文件夹
    /// </summary>
    /// <param name="path">目录路径</param>
    /// <returns>删除操作结果</returns>
    /// <exception cref="Result{bool}">删除失败异常</exception>
    public static Result<bool> DeleteFolder(string path)
    {
        try
        {
            var directoryInfo = new DirectoryInfo(Path.GetFullPath(path));

            foreach (var file in directoryInfo.GetFiles())
                file.Delete();

            foreach (var directory in directoryInfo.GetDirectories())
                DeleteFolder(directory.FullName);

            directoryInfo.Delete();

            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            throw new Result<bool>(e.Message);
        }
    }

    /// <summary>
    /// 将文件转换成byte[]数组
    /// </summary>
    /// <param name="fileUrl">文件路径文件名称</param>
    /// <returns>byte[]数组</returns>
    public static byte[] FileToByte(string fileUrl)
    {
        try
        {
            var fs = new FileStream(fileUrl, FileMode.Open, FileAccess.Read);
            var byteArray = new byte[fs.Length];

            fs.Read(byteArray, 0, byteArray.Length);

            fs.CloseAndDispose();

            return byteArray;
        }
        catch (Exception e)
        {
            throw new Result<bool>(e.Message);
        }
    }

    /// <summary>
    /// 将byte[]数组保存成文件
    /// </summary>
    /// <param name="byteArray">byte[]数组</param>
    /// <param name="fileName">保存至硬盘的文件路径</param>
    /// <returns>保存是否成功</returns>
    public static Result<bool> ByteToFile(byte[] byteArray, string fileName)
    {
        try
        {
            var fs = new FileStream(fileName,
                FileMode.OpenOrCreate, FileAccess.Write);

            fs.Write(byteArray, 0, byteArray.Length);

            fs.CloseAndDispose();

            return new Result<bool>(true);
        }
        catch (Exception e)
        {
            throw new Result<bool>(e.Message);
        }
    }
}

#pragma warning restore IDE0051 // 删除未使用的私有成员
