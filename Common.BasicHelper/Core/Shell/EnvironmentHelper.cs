using System;
using System.IO;
using System.Linq;

namespace Common.BasicHelper.Core.Shell;

public static class EnvironmentHelper
{
    /// <summary>
    /// 获取系统 PATH 中的文件路径
    /// </summary>
    /// <param name="fileName">文件名 (是否包含 .exe 均可)</param>
    /// <returns>准确的文件路径</returns>
    public static string GetFilePathInPaths(string fileName)
    {
        var result = fileName;
        var environmentPaths = Environment.GetEnvironmentVariable("PATH");
        var paths = environmentPaths.Split(';').ToArray();
        var exePath = paths.Select
            (
                x => Path.Combine
                (
                    x,
                    fileName.ToLower().EndsWith(".exe")
                    ?
                    fileName
                    :
                    $"{fileName}.exe"
                )
            )
            .FirstOrDefault(File.Exists);

        if (!string.IsNullOrWhiteSpace(exePath))
            result = exePath;

        return result;
    }
}
