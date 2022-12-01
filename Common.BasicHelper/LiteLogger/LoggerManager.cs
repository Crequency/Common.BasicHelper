﻿using BasicHelper.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicHelper.LiteLogger
{
    public class LoggerManager
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        public enum LogLevel
        {
            Off = 0,
            Fatal = 1,
            Error = 2,
            Warn = 3,
            Info = 4,
            Debug = 5,
            Trace = 6
        }

        /// <summary>
        /// 日志记录器结构
        /// </summary>
        public struct LoggerInfo
        {
            public LoggerInfo(string name_in, string folder, string descr_in = "Nice Logger!",
                LogLevel lv = LogLevel.Error, int lfs = 1024)
            {
                name = name_in;
                descr = descr_in;
                Level = lv;
                limitedFileSize = lfs;

                file = new FileInfo($"{Path.GetFullPath(folder)}/Log_{DateTime.Now:yyyy.MM.dd}_{lv}.log");
                if (!file.Exists) file.Create().Dispose();

                logged_count = 0;
                logged_char_count = 0;
            }

            public LogLevel Level;
            public string name;
            public string descr;
            public ulong logged_count;
            public ulong logged_char_count;

            // File Size in KB
            public int limitedFileSize;

            private readonly FileInfo file;

            /// <summary>
            /// 写入日志
            /// </summary>
            /// <param name="content">内容</param>
            /// <param name="lv">内容等级</param>
            public void Log(string content, LogLevel lv = LogLevel.Error)
            {
                if (lv <= Level)
                {
                    StreamWriter sw = null;
                    try
                    {
                        if (file.Length >= limitedFileSize * 1024)
                            sw = file.CreateText();
                        else
                            sw = file.AppendText();
                        sw.WriteLine($"{DateTime.Now:yyyy.MM.dd-HH:mm:ss}\t{content}");
                        sw.Close();
                    }
                    catch
                    {
                        sw?.Dispose();
                    }
                }
            }

            /// <summary>
            /// 异步写入日志
            /// </summary>
            /// <param name="content">内容</param>
            /// <param name="lv">内容等级</param>
            public async Task LogAsync(string content, LogLevel lv = LogLevel.Error)
            {
                if (lv <= Level)
                {
                    StreamWriter sw = null;
                    try
                    {
                        if (file.Length >= limitedFileSize * 1024)
                            sw = file.CreateText();
                        else
                            sw = file.AppendText();
                        await sw.WriteLineAsync($"{DateTime.Now:yyyy.MM.dd-HH:mm:ss}\t{content}");
                        sw.Close();
                        sw.Dispose();
                    }
                    catch
                    {
                        sw?.Dispose();
                    }
                }
            }

        }

        private readonly Dictionary<string, LoggerInfo> loggerPool = new Dictionary<string, LoggerInfo>();

        /// <summary>
        /// 日志记录器池
        /// </summary>
        public Dictionary<string, LoggerInfo> LoggerPool => loggerPool;

        /// <summary>
        /// 追加日志记录器
        /// </summary>
        /// <param name="name">日志记录器名称</param>
        /// <param name="logger">日志记录器结构</param>
        public void AppendLogger(string name, LoggerInfo logger) => loggerPool.Add(name, logger);

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="logger_name">日志记录器名称</param>
        /// <param name="content">日志内容</param>
        /// <param name="lv">日志记录等级</param>
        public void Log(string logger_name, string content, LogLevel lv = LogLevel.Error)
        {
            if (LoggerPool.ContainsKey(logger_name))
                LoggerPool[logger_name].Log(content, lv);
        }

        /// <summary>
        /// 异步记录日志
        /// </summary>
        /// <param name="logger_name">日志记录器名称</param>
        /// <param name="content">日志内容</param>
        /// <param name="lv">日志记录等级</param>
        public async Task LogAsync(string logger_name, string content, LogLevel lv = LogLevel.Error)
        {
            if (LoggerPool.ContainsKey(logger_name))
                await LoggerPool[logger_name].LogAsync(content, lv);
        }
    }
}
