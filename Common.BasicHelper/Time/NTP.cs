using System;

namespace Common.BasicHelper.Time;

public class NTP
{
    /// <summary>
    /// 获取目标时间与本地时间的差值毫秒数
    /// </summary>
    /// <param name="t1">报文发送时本地时间</param>
    /// <param name="t2">报文送达时远程时间</param>
    /// <param name="t3">报文返回时远程时间</param>
    /// <param name="t4">报文抵达时本地时间</param>
    /// <returns>目标时间与本地时间差值毫秒数</returns>
    public static double GetOffset(DateTime t1, DateTime t2, DateTime t3, DateTime t4)
    {
        //  NTP Protocal
        //  
        //  totalDelay = (t4 - t1) - (t3 - t2)
        //  delay = totalDelay / 2
        //  
        //  { t2 = t1 + offset + delay
        //  { t4 = t3 - offset + delay
        //  
        //  => offset = ((t2 - t1) + (t3 - t4)) / 2

        var offset = (t2 - t1 + (t3 - t4)).TotalMilliseconds;

        return offset;
    }

    /// <summary>
    /// 获取目标时间与本地时间的差值
    /// </summary>
    /// <param name="t1">报文发送时本地时间</param>
    /// <param name="t2">报文送达时远程时间</param>
    /// <param name="t3">报文返回时远程时间</param>
    /// <param name="t4">报文抵达时本地时间</param>
    /// <returns>目标时间与本地时间差值</returns>
    public static TimeSpan GetOffsetTimeSpan(DateTime t1, DateTime t2, DateTime t3, DateTime t4)
        => TimeSpan.FromMilliseconds(GetOffset(t1, t2, t3, t4));
}
