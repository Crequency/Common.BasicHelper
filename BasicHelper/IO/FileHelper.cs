using BasicHelper.StdExp;
using System;
using System.IO;

namespace BasicHelper.IO
{
    public class FileHelper
    {
        /// <summary>
        /// 向指定的路径文件写入内容
        /// </summary>
        /// <param name="path">指定的路径</param>
        /// <param name="content">内容</param>
        /// <returns>写入是否成功以及异常信息</returns>
        public static Exp WriteIn(string path, string content)
        {
            try
            {
                if (File.Exists(path)) File.Delete(path);
                else File.Create(path);
                FileStream fs = new FileStream(path, FileMode.Open);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(content); sw.Flush();
                sw.Close(); sw.Dispose();
                fs.Close(); fs.Dispose();
                return new Exp()
                {
                    OperateResult = true
                };
            }
            catch (Exception o)
            {
                return new Exp()
                {
                    LocalException = o,
                    Title = o.Message,
                    OperateResult = false
                };
            }
        }

        /// <summary>
        /// 向指定路径追加文本，如果路径不存在，则创造该路径
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="content">要追加的内容</param>
        public static void Append(string path, string content) => WriteIn(path, $"{ReadAll(path)}\n{content}");

        /// <summary>
        /// 以二进制流写入指定路径全部内容
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="content">内容</param>
        /// <returns>异常信息</returns>
        public static Exp WriteByteIn(string path, byte[] content)
        {
            try
            {
                if (!File.Exists(path))
                {
                    File.Create(path);
                }
                FileStream fs = new FileStream(path, FileMode.Open);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(content);
                bw.Flush();
                bw.Close(); bw.Dispose();
                fs.Close(); fs.Dispose();
                return new Exp()
                {
                    OperateResult = true
                };
            }
            catch (Exception p)
            {
                return new Exp()
                {
                    OperateResult = false,
                    LocalException = p,
                    Title = p.Message
                };
            }
        }

        /// <summary>
        /// 以二进制流写入指定路径全部内容
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="content">内容</param>
        public static void WriteBytesToFile(string path, byte[] content)
        {
            FileStream fs_write = new FileStream(path, FileMode.Open);
            fs_write.Write(content, 0, content.Length);
            fs_write.Close(); fs_write.Dispose();
        }

        /// <summary>
        /// 读取指定路径的全部内容
        /// </summary>
        /// <param name="path">指定路径</param>
        /// <returns>内容或异常信息</returns>
        public static string ReadAll(string path)
        {
            string content;
            if (File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                content = sr.ReadToEnd();
                sr.Close(); sr.Dispose();
                fs.Close(); fs.Dispose();
                return content;
            }
            else
            {
                return "File didn't exists.";
            }
        }

        /// <summary>
        /// 以二进制流读取指定路径全部内容
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>二进制流</returns>
        public static byte[] ReadByteAll(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            byte[] byData = br.ReadBytes((int)fs.Length);
            br.Close(); br.Dispose();
            fs.Close(); fs.Dispose();
            return byData;
        }

        /// <summary>
        /// 二进制流读取文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>二进制流</returns>
        private static byte[] FileToBytes(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            byte[] buffer = new byte[fi.Length];
            FileStream fs = fi.OpenRead();
            fs.Read(buffer, 0, Convert.ToInt32(fi.Length));
            fs.Close(); fs.Dispose();
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
            {
                File.Delete(newFilePath);
            }
            FileStream fs = new FileStream(newFilePath, FileMode.CreateNew);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(fileBuffer, 0, fileBuffer.Length); // 用文件流生成一个文件
            bw.Close(); bw.Dispose();
            fs.Close(); fs.Dispose();
        }

        /// <summary>
        /// 循环删除文件夹、子文件夹及其文件
        /// </summary>
        /// <param name="file">目录</param>
        public static void DeleteSrcFolder(string file)
        {
            DirectoryInfo fileInfo = new DirectoryInfo(file);
            fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;
            File.SetAttributes(file, System.IO.FileAttributes.Normal);
            if (Directory.Exists(file))
            {
                foreach (string f in Directory.GetFileSystemEntries(file))
                {
                    if (File.Exists(f))
                        File.Delete(f);
                    else
                        DeleteSrcFolder1(f);
                }
            }
        }

        private static void DeleteSrcFolder1(string file)
        {
            DirectoryInfo fileInfo = new DirectoryInfo(file);
            fileInfo.Attributes = FileAttributes.Normal & FileAttributes.Directory;
            File.SetAttributes(file, FileAttributes.Normal);
            if (Directory.Exists(file))
            {
                foreach (string f in Directory.GetFileSystemEntries(file))
                {
                    if (File.Exists(f))
                        File.Delete(f);
                    else
                        DeleteSrcFolder1(f);
                }
                Directory.Delete(file);
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
                using (FileStream fs = new FileStream(fileUrl, FileMode.Open, FileAccess.Read))
                {
                    byte[] byteArray = new byte[fs.Length];
                    fs.Read(byteArray, 0, byteArray.Length);
                    return byteArray;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 将byte[]数组保存成文件
        /// </summary>
        /// <param name="byteArray">byte[]数组</param>
        /// <param name="fileName">保存至硬盘的文件路径</param>
        /// <returns>保存是否成功</returns>
        public static bool ByteToFile(byte[] byteArray, string fileName)
        {
            bool result = false;
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(byteArray, 0, byteArray.Length);
                    result = true;
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
