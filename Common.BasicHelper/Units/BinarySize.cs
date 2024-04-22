using System.Text.RegularExpressions;
using Common.BasicHelper.Utils.Extensions;

namespace Common.BasicHelper.Units;

public partial class BinarySize
{
    public BinarySize()
    {

    }

    public BinarySize(long bytesCount)
    {
        BytesCount = bytesCount;
    }

    public bool IsInIecBase { get; set; } = false;

    public long BytesCount { get; set; } = 0;

    public string DisplayText
    {
        get
        {
            var lvBase = IsInIecBase ? 1000.0 : 1024.0;

            var lvB = 1.0;
            var lvKB = lvB * lvBase;
            var lvMB = lvKB * lvBase;
            var lvGB = lvMB * lvBase;
            var lvTB = lvGB * lvBase;
            var lvPB = lvTB * lvBase;
            var lvEB = lvPB * lvBase;
            var lvZB = lvEB * lvBase;

            var iecBase = IsInIecBase ? "i" : "";

            if (BytesCount < lvKB)
                return $"{BytesCount} B";

            if (BytesCount < lvMB)
                return $"{System.Math.Round(BytesCount / lvKB, 2)} K{iecBase}B";

            if (BytesCount < lvGB)
                return $"{System.Math.Round(BytesCount / lvMB, 2)} M{iecBase}B";

            if (BytesCount < lvTB)
                return $"{System.Math.Round(BytesCount / lvGB, 2)} G{iecBase}B";

            if (BytesCount < lvPB)
                return $"{System.Math.Round(BytesCount / lvTB, 2)} T{iecBase}B";

            if (BytesCount < lvEB)
                return $"{System.Math.Round(BytesCount / lvPB, 2)} P{iecBase}B";

            if (BytesCount < lvZB)
                return $"{System.Math.Round(BytesCount / lvEB, 2)} E{iecBase}B";

            return $"{System.Math.Round(BytesCount / lvZB, 2)} Z{iecBase}B";
        }
    }

    public static BinarySize? Parse(string size)
    {
        BinarySize? result = null;

        var regex = new Regex(@"(\d+).?(\d*)\s*(B|Ki?B|Mi?B|Gi?B|Ti?B|Pi?B|Ei?B)", RegexOptions.IgnoreCase);

        regex.Match(size).WhenSuccess(x =>
        {
            result = new();

            var integer = x?.Groups[1].Value ?? string.Empty;
            var left = x?.Groups[2].Value ?? string.Empty;
            var unit = x?.Groups[3].Value ?? string.Empty;

            var diff = unit.ToLower().Contains('i') ? 1000 : 1024;

            result.IsInIecBase = diff == 1000;

            var scale = unit.ToUpper().Replace("IB", "B") switch
            {
                "B" => 1,
                "KB" => diff,
                "MB" => diff * diff,
                "GB" => System.Math.Pow(diff, 3),
                "TB" => System.Math.Pow(diff, 4),
                "PB" => System.Math.Pow(diff, 5),
                "EB" => System.Math.Pow(diff, 6),
                _ => 1,
            };

            if (unit.Contains('b')) scale *= 1.0 / 8.0;

            var p = 0;

            for (var i = integer.Length - 1; i >= 0; --i, ++p)
                result.BytesCount += (long)((integer[i] - '0') * System.Math.Pow(10, p) * scale);

            p = -1;

            for (var i = 0; i < left.Length; i++, --p)
                result.BytesCount += (long)((integer[i] - '0') * System.Math.Pow(10, p) * scale);
        });

        return result;
    }

    public static BinarySize operator +(BinarySize? a, BinarySize? b) => new((a?.BytesCount ?? 0) + (b?.BytesCount ?? 0));

    public static BinarySize operator -(BinarySize? a, BinarySize? b) => new((a?.BytesCount ?? 0) - (b?.BytesCount ?? 0));

    public static long operator *(BinarySize? a, BinarySize? b) => (a?.BytesCount ?? 0) * (b?.BytesCount ?? 1);

    public static double operator /(BinarySize? a, BinarySize? b) => ((a?.BytesCount * 1.0) ?? 0.0) / ((b?.BytesCount * 1.0) ?? 1.0);

    public static bool operator <(BinarySize a, BinarySize b) => a.BytesCount < b.BytesCount;

    public static bool operator <=(BinarySize a, BinarySize b) => a.BytesCount <= b.BytesCount;

    public static bool operator >(BinarySize a, BinarySize b) => a.BytesCount > b.BytesCount;

    public static bool operator >=(BinarySize a, BinarySize b) => a.BytesCount >= b.BytesCount;
}
