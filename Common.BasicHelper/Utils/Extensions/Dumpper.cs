using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Common.BasicHelper.Utils.Extensions;

public static class Dumpper
{
    public static string Dump<T>(this Queue<T> queue, string separator = " ", bool print = true) =>
        string.Join(separator, queue.Dump2Lines(print: print));

    public static string[] Dump2Lines<T>(this Queue<T> queue, bool print = true)
    {
        var result = new List<string>();
        queue.ForEach(x => result.Add(x?.ToString() ?? string.Empty), true);
        if (print)
            result.ToArray().Print();
        return [.. result];
    }

    public static string Dump(this NetworkInterface adapter, bool print = true) =>
        string.Join(Environment.NewLine, adapter.Dump2Lines(print: print));

    public static string[] Dump2Lines(this NetworkInterface adapter, bool print = true)
    {
        var sb = new List<string>();
        var adapterProperties = adapter.GetIPProperties();
        var iPv4Statistics = adapter.GetIPv4Statistics();
        if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
        {
            sb.Add($"网络适配器名称: {adapter.Name}");
            sb.Add($"网络适配器标识符: {adapter.Id}");
            sb.Add($"适配器连接状态: {adapter.OperationalStatus}");
            if (adapterProperties.UnicastAddresses.Count > 0)
            {
                var unicastIp = adapterProperties.UnicastAddresses[0];
                sb.Add($"IP 地址: {unicastIp.Address}");
                sb.Add($"子网掩码: {unicastIp.IPv4Mask}");
            }
            if (adapterProperties.GatewayAddresses.Count > 0)
            {
                var gatewayIp = adapterProperties.GatewayAddresses[0];
                sb.Add($"默认网关:{gatewayIp.Address}"); //默认网关
            }
            int dnsCount = adapterProperties.DnsAddresses.Count;
            sb.Add("DNS 服务器地址:"); //默认网关
            if (dnsCount > 0)
            {
                //其中第一个为首选 DNS，第二个为备用的，余下的为所有DNS为DNS备用，按使用顺序排列
                for (int i = 0; i < dnsCount; i++)
                    sb.Add($"{adapterProperties.DnsAddresses[i],50}");
            }
            sb.Add($"网络接口速度: {adapter.Speed / 1000000:0.0}Mbps");
            sb.Add($"接口描述: {adapter.Description}");
            sb.Add($"适配器的媒体访问控制 (MAC) 地址: {adapter.GetPhysicalAddress()}");
            sb.Add($"该接口是否只接收数据包: {adapter.IsReceiveOnly}");
            sb.Add($"该接口收到的字节数: {iPv4Statistics.BytesReceived}");
            sb.Add($"该接口发送的字节数: {iPv4Statistics.BytesSent}");
            sb.Add($"该接口丢弃的传入数据包数: {iPv4Statistics.IncomingPacketsDiscarded}");
            sb.Add($"该接口丢弃的传出数据包数: {iPv4Statistics.OutgoingPacketsDiscarded}");
            sb.Add($"IP 地址: {iPv4Statistics.IncomingPacketsWithErrors}");
            sb.Add($"IP 地址: {iPv4Statistics.OutgoingPacketsWithErrors}");
            sb.Add($"IP 地址: {iPv4Statistics.IncomingUnknownProtocolPackets}");
            sb.Add(new StringBuilder().Append('-', 50).ToString());
        }

        if (print)
            sb.ToArray().Print();

        return [.. sb];
    }

    public static string Print<T>(this T? src, bool print = true)
    {
        if (print)
            Console.WriteLine(src?.ToString());

        return src?.ToString() ?? "";
    }

    public static string Print<T>(
        this IEnumerable<T>? array,
        string? connection = ", ",
        bool cutEndingConnection = true,
        bool separateWithNewLine = true,
        bool print = true
    )
    {
        if (array is null)
            return string.Empty;

        if (array is Queue<T> queue)
            return Dump(queue, connection ?? " ", print);

        var sb = new StringBuilder();

        var useNewLine2ReplaceConnectionString =
            separateWithNewLine && array is IEnumerable<string>;

        foreach (var item in array)
        {
            sb.Append(item);

            sb.Append(useNewLine2ReplaceConnectionString ? Environment.NewLine : connection);
        }

        var result = sb.ToString();

        if (useNewLine2ReplaceConnectionString)
        {
            if (print)
                Console.WriteLine(result);

            return result;
        }

        if (cutEndingConnection)
            result = result[..(sb.Length - connection?.Length ?? 0)];

        if (print)
            Console.WriteLine(result);

        return result;
    }
}
