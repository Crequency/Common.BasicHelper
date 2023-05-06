using Common.BasicHelper.Utils.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Common.BasicHelper.IO;

public class DirectoryHelper
{
    /// <summary>
    /// 获取一个文件夹包括子文件夹及其子文件的总大小
    /// </summary>
    /// <param name="path">文件夹路径</param>
    /// <returns>总大小</returns>
    public static long GetDirectorySize(string path)
    {
        var result = 0L;

        var dir = path.GetFullPath();

        if (!Directory.Exists(dir)) return result;

        var pendingFolders = new Queue<DirectoryInfo>();

        pendingFolders.Enqueue(new DirectoryInfo(dir));

        while (pendingFolders.Count > 0)
        {
            var folder = pendingFolders.Dequeue();

            result += folder.GetFiles().Sum(file => file.Length);

            foreach (var subFolder in folder.GetDirectories())
                pendingFolders.Enqueue(subFolder);
        }

        return result;
    }
}
