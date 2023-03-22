using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Common.BasicHelper.Util.Extension;

public static class Dumpper
{
    /// <summary>
    /// 队列打印机
    /// </summary>
    /// <typeparam name="T">队列类型</typeparam>
    /// <param name="queue">队列</param>
    /// <param name="separater">分隔符</param>
    /// <returns>打印内容</returns>
    public static string Dump<T>(this Queue<T> queue, string separater = " ")
    {
        var result = new StringBuilder();
        queue.ForEach(x =>
        {
            result.Append(x.ToString());
            result.Append(separater);
        }, true);
        return result.ToString();
    }

    /// <summary>
    /// 队列打印机, 按行返回
    /// </summary>
    /// <typeparam name="T">队列类型</typeparam>
    /// <param name="queue">队列</param>
    /// <returns>打印内容</returns>
    public static string[] Dump2Lines<T>(this Queue<T> queue)
    {
        var result = new List<string>();
        queue.ForEach(x => result.Add(x.ToString()), true);
        return result.ToArray();
    }

    /// <summary>
    /// 网络适配器打印机
    /// </summary>
    /// <param name="adapter">网络适配器</param>
    /// <returns>打印内容</returns>
    public static string Dump(this NetworkInterface adapter)
    {
        var sb = new StringBuilder();
        var adapterProperties = adapter.GetIPProperties();
        var v4s = adapter.GetIPv4Statistics();
        if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
        {
            sb.AppendLine($"{"网络适配器名称: "}{adapter.Name}");
            sb.AppendLine($"{"网络适配器标识符: "}{adapter.Id}");
            sb.AppendLine($"{"适配器连接状态: "}{adapter.OperationalStatus}");
            if (adapterProperties.UnicastAddresses.Count > 0)
            {
                var unicastIP = adapterProperties.UnicastAddresses[0];
                sb.AppendLine($"{"IP地址:"}{unicastIP.Address}");
                sb.AppendLine($"{"子网掩码:"}{unicastIP.IPv4Mask}");
            }
            if (adapterProperties.GatewayAddresses.Count > 0)
            {
                var gatewayIP = adapterProperties.GatewayAddresses[0];
                sb.AppendLine($"默认网关:{gatewayIP.Address}");   //默认网关
            }
            int DnsCount = adapterProperties.DnsAddresses.Count;
            sb.AppendLine("DNS服务器地址:");   //默认网关
            if (DnsCount > 0)
            {
                //其中第一个为首选DNS，第二个为备用的，余下的为所有DNS为DNS备用，按使用顺序排列
                for (int i = 0; i < DnsCount; i++)
                    sb.AppendLine($"{adapterProperties.DnsAddresses[i],50}");
            }
            sb.AppendLine($"{"网络接口速度: "}{adapter.Speed / 1000000:0.0}Mbps");
            sb.AppendLine($"{"接口描述: "}{adapter.Description}");
            sb.AppendLine($"{"适配器的媒体访问控制 (MAC) 地址: "}{adapter.GetPhysicalAddress()}");
            sb.AppendLine($"{"该接口是否只接收数据包: "}{adapter.IsReceiveOnly}");
            sb.AppendLine($"{"该接口收到的字节数: "}{v4s.BytesReceived}");
            sb.AppendLine($"{"该接口发送的字节数: "}{v4s.BytesSent}");
            sb.AppendLine($"{"该接口丢弃的传入数据包数: "}{v4s.IncomingPacketsDiscarded}");
            sb.AppendLine($"{"该接口丢弃的传出数据包数: "}{v4s.OutgoingPacketsDiscarded}");
            sb.AppendLine($"{"IP地址: "}{v4s.IncomingPacketsWithErrors}");
            sb.AppendLine($"{"IP地址: "}{v4s.OutgoingPacketsWithErrors}");
            sb.AppendLine($"{"IP地址: "}{v4s.IncomingUnknownProtocolPackets}");
            sb.AppendLine(new StringBuilder().Append('-', 50).ToString());
        }

        return sb.ToString();
    }

    /// <summary>
    /// 网络适配器打印机, 按行返回
    /// </summary>
    /// <param name="adapter">网络适配器</param>
    /// <returns>打印内容</returns>
    public static string[] Dump2Lines(this NetworkInterface adapter)
    {
        var sb = new List<string>();
        var adapterProperties = adapter.GetIPProperties();
        var v4s = adapter.GetIPv4Statistics();
        if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
        {
            sb.Add($"{"网络适配器名称: "}{adapter.Name}");
            sb.Add($"{"网络适配器标识符: "}{adapter.Id}");
            sb.Add($"{"适配器连接状态: "}{adapter.OperationalStatus}");
            if (adapterProperties.UnicastAddresses.Count > 0)
            {
                var unicastIP = adapterProperties.UnicastAddresses[0];
                sb.Add($"{"IP地址:"}{unicastIP.Address}");
                sb.Add($"{"子网掩码:"}{unicastIP.IPv4Mask}");
            }
            if (adapterProperties.GatewayAddresses.Count > 0)
            {
                var gatewayIP = adapterProperties.GatewayAddresses[0];
                sb.Add($"默认网关:{gatewayIP.Address}");   //默认网关
            }
            int DnsCount = adapterProperties.DnsAddresses.Count;
            sb.Add("DNS服务器地址:");   //默认网关
            if (DnsCount > 0)
            {
                //其中第一个为首选DNS，第二个为备用的，余下的为所有DNS为DNS备用，按使用顺序排列
                for (int i = 0; i < DnsCount; i++)
                    sb.Add($"{adapterProperties.DnsAddresses[i],50}");
            }
            sb.Add($"{"网络接口速度: "}{adapter.Speed / 1000000:0.0}Mbps");
            sb.Add($"{"接口描述: "}{adapter.Description}");
            sb.Add($"{"适配器的媒体访问控制 (MAC) 地址: "}{adapter.GetPhysicalAddress()}");
            sb.Add($"{"该接口是否只接收数据包: "}{adapter.IsReceiveOnly}");
            sb.Add($"{"该接口收到的字节数: "}{v4s.BytesReceived}");
            sb.Add($"{"该接口发送的字节数: "}{v4s.BytesSent}");
            sb.Add($"{"该接口丢弃的传入数据包数: "}{v4s.IncomingPacketsDiscarded}");
            sb.Add($"{"该接口丢弃的传出数据包数: "}{v4s.OutgoingPacketsDiscarded}");
            sb.Add($"{"IP地址: "}{v4s.IncomingPacketsWithErrors}");
            sb.Add($"{"IP地址: "}{v4s.OutgoingPacketsWithErrors}");
            sb.Add($"{"IP地址: "}{v4s.IncomingUnknownProtocolPackets}");
            sb.Add(new StringBuilder().Append('-', 50).ToString());
        }
        return sb.ToArray();
    }
}
