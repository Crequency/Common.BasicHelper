using System;
using System.Runtime.InteropServices;
using System.Threading;

#pragma warning disable IDE0052 // 删除未读的私有成员

namespace BasicHelper.Windows
{
    public class MutexHelper
    {
        /// <summary>
        /// 恢复窗口
        /// </summary>
        /// <param name="windowTitle">窗口标题</param>
        public static void Restore(string windowTitle)
        {
            IntPtr hWndPtr = FindWindow(string.Empty, windowTitle);
            ShowWindow(hWndPtr, SW_RESTORE);
            SetForegroundWindow(hWndPtr);
        }

        /// <summary>
        /// 检查应用程序是否在进程中已经存在了
        /// </summary>
        /// <param name="name">工程命名空间</param>
        public static bool CheckApplicationMutex(string? name)
        {
            // 第二个参数为 你的工程命名空间名。
            // out 给 ret 为 false 时，表示已有相同实例运行。
            _ = new Mutex(true, name, out bool mutexResult);
            return mutexResult;
        }

        #region 扩展库函数导入 [DLLImport USER32.dll]

        public const int SW_RESTORE = 9;

        /// <summary>
        /// 在桌面窗口列表中寻找与指定条件相符的第一个窗口。
        /// </summary>
        /// <param name="lpClassName">指向指定窗口的类名。如果 lpClassName 是 NULL，所有类名匹配。</param>
        /// <param name="lpWindowName">指向指定窗口名称(窗口的标题）。如果 lpWindowName 是 NULL，所有windows命名匹配。</param>
        /// <returns>返回指定窗口句柄</returns>
        [DllImport("USER32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 将窗口还原,可从最小化还原
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="nCmdShow"></param>
        /// <returns></returns>
        [DllImport("USER32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// 激活指定窗口
        /// </summary>
        /// <param name="hWnd">指定窗口句柄</param>
        /// <returns></returns>
        [DllImport("USER32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        #endregion
    }
}

#pragma warning restore IDE0052 // 删除未读的私有成员