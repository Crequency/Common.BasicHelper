using System;
using System.IO;
using System.Linq;

namespace Common.BasicHelper.Core.Shell;

public static class EnvironmentHelper
{
    /// <summary>
    /// Get file path from system environment variable "Path"
    /// </summary>
    /// <param name="fileName">File name (With or without ".exe" are available)</param>
    /// <returns>Exactly file path</returns>
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

public static class EnvironmentHelperExtensions
{
    public static string FindInPath(this string fileName) => EnvironmentHelper.GetFilePathInPaths(fileName);
}
