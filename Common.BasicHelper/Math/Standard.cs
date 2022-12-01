using System;

namespace Common.BasicHelper.Math
{
    public class Standard
    {
        /// <summary>
        /// 比较两个变量的大小, 找出较小者
        /// </summary>
        /// <typeparam name="T">可比较的类型</typeparam>
        /// <param name="a">变量 a</param>
        /// <param name="b">变量 b</param>
        /// <returns>两者中较小的一个</returns>
        public static T Min<T>(T a, T b) where T : IComparable<T> => a.CompareTo(b) < 0 ? a : b;

        /// <summary>
        /// 在列表中找到最小的一个
        /// </summary>
        /// <typeparam name="T">可比较的类型</typeparam>
        /// <param name="input">输入列表</param>
        /// <returns>最小的一个</returns>
        public static T Min<T>(params T[] input) where T : IComparable<T>
        {
            T min = input[0];
            for (int i = 1; i < input.Length; i++)
                min = Min(min, input[i]);
            return min;
        }

        /// <summary>
        /// 比较两个变量的大小, 找出较大者
        /// </summary>
        /// <typeparam name="T">可比较的类型</typeparam>
        /// <param name="a">变量 a</param>
        /// <param name="b">变量 b</param>
        /// <returns>两者中较大的一个</returns>
        public static T Max<T>(T a, T b) where T : IComparable<T> => a.CompareTo(b) > 0 ? a : b;

        /// <summary>
        /// 在列表中找到最大的一个
        /// </summary>
        /// <typeparam name="T">可比较的类型</typeparam>
        /// <param name="input">输入列表</param>
        /// <returns>最大的一个</returns>
        public static T Max<T>(params T[] input) where T : IComparable<T>
        {
            T max = input[0];
            for (int i = 1; i < input.Length; i++)
                max = Max(max, input[i]);
            return max;
        }
    }
}
