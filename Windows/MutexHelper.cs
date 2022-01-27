using System;
using System.Runtime.InteropServices;
using System.Threading;

#pragma warning disable IDE0052 // 删除未读的私有成员

namespace BasicHelper.Windows
{
    public class MutexHelper
    {
        /// <summary>
        /// 进程
        /// </summary>
        private static Mutex mutex;

        public static void Restore()
        {
            // 找到已经在运行的实例句柄(给出你的窗体标题名 “Deamon Club”)
            IntPtr hWndPtr = FindWindow(null, "KitX");

            // 还原窗口
            ShowWindow(hWndPtr, SW_RESTORE);

            // 激活窗口
            SetForegroundWindow(hWndPtr);
        }

        /// <summary>
        /// 检查应用程序是否在进程中已经存在了
        /// </summary>
        public static bool CheckApplicationMutex()
        {
            // 第二个参数为 你的工程命名空间名。
            // out 给 ret 为 false 时，表示已有相同实例运行。
            mutex = new Mutex(true, "KitX", out bool mutexResult);
            return mutexResult;
        }

        #region Windows API

        // ShowWindow 参数  
        public const int SW_RESTORE = 9;

        /// <summary>
        /// 在桌面窗口列表中寻找与指定条件相符的第一个窗口。
        /// </summary>
        /// <param name="lpClassName">指向指定窗口的类名。如果 lpClassName 是 NULL，所有类名匹配。</param>
        /// <param name="lpWindowName">指向指定窗口名称(窗口的标题）。如果 lpWindowName 是 NULL，所有windows命名匹配。</param>
        /// <returns>返回指定窗口句柄</returns>
        [DllImport("USER32.DLL", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 将窗口还原,可从最小化还原
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [DllImport("USER32.DLL")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// 激活指定窗口
        /// </summary>
        /// <param name="hWnd">指定窗口句柄</param>
        /// <returns></returns>
        [DllImport("USER32.DLL")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        #endregion
    }
}

#pragma warning restore IDE0052 // 删除未读的私有成员