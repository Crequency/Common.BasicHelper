using System;
using System.Collections.Generic;

namespace Common.BasicHelper.Util.Extension
{
    public static class QueueHelper
    {
        /// <summary>
        /// 入队一个元素并返回队列本身
        /// </summary>
        /// <typeparam name="T">队列类型</typeparam>
        /// <param name="queue">队列</param>
        /// <param name="item">元素</param>
        /// <returns>队列</returns>
        public static Queue<T> Push<T>(this Queue<T> queue, T item)
        {
            queue.Enqueue(item);
            return queue;
        }

        /// <summary>
        /// 出队一个元素并返回队列本身
        /// </summary>
        /// <typeparam name="T">队列类型</typeparam>
        /// <param name="queue">队列</param>
        /// <returns>队列</returns>
        public static Queue<T> Pop<T>(this Queue<T> queue)
        {
            _ = queue.Dequeue();
            return queue;
        }

        /// <summary>
        /// 判断队列是否为空
        /// </summary>
        /// <typeparam name="T">队列类型</typeparam>
        /// <param name="queue">队列</param>
        /// <returns>是否为空</returns>
        public static bool IsEmpty<T>(this Queue<T> queue) => queue.Count == 0;

        /// <summary>
        /// 判断队列是否为空
        /// </summary>
        /// <typeparam name="T">队列类型</typeparam>
        /// <param name="queue">队列</param>
        /// <returns>是否为空</returns>
        public static bool IsNotEmpty<T>(this Queue<T> queue) => queue.Count > 0;

        /// <summary>
        /// 遍历队列对每一个元素执行操作, 执行完毕后返回队列本身
        /// </summary>
        /// <typeparam name="T">队列类型</typeparam>
        /// <param name="queue">队列</param>
        /// <param name="action">对元素的操作</param>
        /// <param name="reappend">是否将出队元素重新入队</param>
        /// <param name="locker">操作锁</param>
        /// <returns>队列本身</returns>
        public static Queue<T> ForEach<T>(this Queue<T> queue, Action<T> action,
            bool reappend = false, object locker = null)
        {
            Queue<T> func()
            {
                var count = queue.Count;
                while (count > 0)
                {
                    var item = queue.Dequeue();
                    action.Invoke(item);
                    --count;
                    if (reappend) queue.Enqueue(item);
                }
                return queue;
            }

            if (locker != null)
            {
                lock (locker)
                {
                    return func();
                }
            }
            else return func();
        }
    }
}
