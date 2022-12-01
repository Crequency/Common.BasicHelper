using System;
using System.IO;
using System.Text;

namespace Common.BasicHelper.IO.PipeHelper
{
    public class StreamString : IDisposable
    {
        private readonly Stream ioStream;
        private readonly UnicodeEncoding streamEncoding;

        public StreamString(Stream ioStream)
        {
            this.ioStream = ioStream;
            streamEncoding = new UnicodeEncoding();
        }

        public void Dispose()
        {
            ioStream.Close();
            ioStream.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 读取字符串
        /// </summary>
        /// <returns>字符串</returns>
        public string ReadString()
        {
            int len = ioStream.ReadByte() * 256;
            len += ioStream.ReadByte();
            byte[] inBuffer = new byte[len];
            ioStream.Read(inBuffer, 0, len);

            return streamEncoding.GetString(inBuffer);
        }

        /// <summary>
        /// 写入字符串
        /// </summary>
        /// <param name="outString">字符串</param>
        /// <returns>写出缓冲区长度</returns>
        public int WriteString(string outString)
        {
            byte[] outBuffer = streamEncoding.GetBytes(outString);
            int len = outBuffer.Length;
            if (len > ushort.MaxValue)
            {
                len = ushort.MaxValue;
            }
            ioStream.WriteByte((byte)(len / 256));
            ioStream.WriteByte((byte)(len & 255));
            ioStream.Write(outBuffer, 0, len);
            ioStream.Flush();

            return outBuffer.Length + 2;
        }
    }
}
