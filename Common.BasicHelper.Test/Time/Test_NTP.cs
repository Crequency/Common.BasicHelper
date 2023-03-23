using Common.BasicHelper.Time;
using System.Text;

namespace Common.BasicHelper.Test.Time;

[TestClass]
public class Test_NTP
{
    private static double GetOffsetsMillisecondsInRandomWay(bool useTimeSpan = false, bool print = true)
    {
        var random = new Random();

        //  发送时网络延迟
        var networkInterval_1 = TimeSpan.FromMilliseconds(random.Next(1, 1000));

        //  接收时网络延迟
        var networkInterval_2 = TimeSpan.FromMilliseconds(random.Next(1, 1000));

        //  真实时间差
        var actualOffset = TimeSpan.FromSeconds(random.Next(1, 100));

        //  本地发送时时间戳
        var localSend = DateTime.Now;

        //  远程接收时时间戳
        var remoteReceive = localSend + networkInterval_1 + actualOffset;

        //  远程发送时时间戳
        var remoteSend = remoteReceive;

        //  本地接收时时间戳
        var localReceive = remoteSend + networkInterval_2;

        //  计算时间差毫秒数
        var calculateOffsetMilliseconds = NTP
            .GetOffset(localSend, remoteReceive, remoteSend, localReceive);

        //  计算时间差
        var calculateOffset = useTimeSpan
            ? NTP.GetOffsetTimeSpan(localSend, remoteReceive, remoteSend, localReceive)
            : TimeSpan.FromMilliseconds(calculateOffsetMilliseconds);

        //  计算时间差与真实时间差的差值
        var offsetsDelta = calculateOffset - actualOffset;

        //  计算时间差与真实时间差的差值的毫秒数的绝对值
        var offsetsDeltaMilliseconds = System.Math.Abs(offsetsDelta.TotalMilliseconds);

        if (print)
        {
            static string ToTotalTimeString(DateTime time) => time.ToString("O");

            var sb = new StringBuilder();

            sb.AppendLine($"Local sending time: {ToTotalTimeString(localSend)}");
            sb.AppendLine($"Network delayed: {networkInterval_1.TotalMilliseconds} ms");
            sb.AppendLine($"Remote receiving time: {ToTotalTimeString(remoteReceive)}");
            sb.AppendLine($"Remote sending time: {ToTotalTimeString(remoteSend)}");
            sb.AppendLine($"Network delayed: {networkInterval_2.TotalMilliseconds} ms");
            sb.AppendLine($"Local receiving time: {ToTotalTimeString(localReceive)}");
            sb.AppendLine();
            sb.AppendLine($"Actual offset: {actualOffset}");
            sb.AppendLine($"Calculate offset: {calculateOffset}");
            sb.AppendLine($"Delta of two offsets: {offsetsDeltaMilliseconds} ms");

            Console.WriteLine(sb.ToString());
        }

        return offsetsDeltaMilliseconds;
    }

    [TestMethod]
    public void Test_GetOffset()
    {
        var offsetsDeltaMilliseconds = GetOffsetsMillisecondsInRandomWay();

        Assert.IsTrue(offsetsDeltaMilliseconds < 1200);
    }

    [TestMethod]
    public void Test_GetOffsetTimeSpan()
    {
        var offsetsDeltaMilliseconds = GetOffsetsMillisecondsInRandomWay(
            useTimeSpan: true
        );

        Assert.IsTrue(offsetsDeltaMilliseconds < 1200);
    }

    [TestMethod]
    public void BenchMark_GetOffset()
    {
        var totalOffsetsDeltaMilliseconds = 0.0;

        var tasksCount = 1000;

        //var addLock = new object();

        //var tasks = new List<Task>();

        //foreach (var task in Enumerable.Range(1, tasksCount))
        //    tasks.Add(new Task(() =>
        //    {
        //        var offsetsDeltaMilliseconds = GetOffsetsMillisecondsInRandomWay();

        //        lock (addLock)
        //        {
        //            totalOffsetsDeltaMilliseconds += offsetsDeltaMilliseconds;
        //        }
        //    }));

        //await Task.WhenAll(tasks);

        foreach (var task in Enumerable.Range(1, tasksCount))
            totalOffsetsDeltaMilliseconds += GetOffsetsMillisecondsInRandomWay(print: false);

        var averageMilliseconds = totalOffsetsDeltaMilliseconds / tasksCount;

        Console.WriteLine($"Tasks count: {tasksCount}");
        Console.WriteLine($"Average offsets delta: {averageMilliseconds} ms");

        Assert.IsTrue(averageMilliseconds < 1200);
    }
}
