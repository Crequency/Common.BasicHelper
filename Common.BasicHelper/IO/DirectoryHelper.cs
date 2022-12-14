using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Common.BasicHelper.IO
{
    public class DirectoryHelper
    {
        /// <summary>
        /// 获取一个文件夹包括子文件夹及其子文件的总大小
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <returns>总大小</returns>
        public static long GetDirectorySize(string path)
        {
            var dir = Path.GetFullPath(path);
            if (!Directory.Exists(dir)) return 0;
            var dirs2For = new Queue<DirectoryInfo>();
            dirs2For.Enqueue(new DirectoryInfo(dir));
            long totalSize = 0;
            while (dirs2For.Count > 0)
            {
                var folder = dirs2For.Dequeue();
                totalSize += folder.GetFiles().Sum(file => file.Length);
                foreach (var subDir in folder.GetDirectories()) dirs2For.Enqueue(subDir);
            }

            return totalSize;
        }
    }
}