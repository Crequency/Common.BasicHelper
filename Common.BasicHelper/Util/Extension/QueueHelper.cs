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
        public static bool IsEmpty<T>(this Queue<T> queue) => queue.Count > 0;
    }
}
