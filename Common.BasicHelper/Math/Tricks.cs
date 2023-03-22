using System.Text;

namespace Common.BasicHelper.Math;

public class Tricks
{
    /// <summary>
    /// 获取一个乘法表
    /// </summary>
    /// <param name="from">乘法表从哪开始</param>
    /// <param name="to">乘法表从哪结束</param>
    /// <param name="direction">方向, true 为正三角, false 为反三角, 默认为正</param>
    /// <returns>一个分行的乘法表</returns>
    public static string GetMultiplicationTable(int from, int to, bool direction = true)
    {
        var sb = new StringBuilder();

        if (direction)
        {
            for (var x = from; x <= to; ++x)
            {
                for (var y = from; y <= x; ++y)
                    sb.Append($"{y}*{x}={x * y}\t");
                sb.AppendLine();
            }
        }
        else
        {
            for (var x = from; x >= from; --x)
            {
                for (var y = from; y <= x; ++y)
                    sb.Append($"{y}*{x}={x * y}\t");
                sb.AppendLine();
            }
        }

        return sb.ToString();
    }
}

public static class TricksExtensions
{
    /// <summary>
    /// 获取一个乘法表
    /// </summary>
    /// <param name="from">乘法表从哪开始</param>
    /// <param name="to">乘法表从哪结束</param>
    /// <param name="direction">方向, true 为正三角, false 为反三角, 默认为正</param>
    /// <returns>一个分行的乘法表</returns>
    public static string GetMultiplicationTableTo(this int from, int to, bool direction = true)
        => Tricks.GetMultiplicationTable(from, to, direction);
}
