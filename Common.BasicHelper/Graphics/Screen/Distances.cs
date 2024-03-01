using System;
using System.Linq;
using System.Text.RegularExpressions;
using Common.BasicHelper.Utils.Extensions;

namespace Common.BasicHelper.Graphics.Screen;

public class Distances
{
    public double Left { get; set; }

    public double Right { get; set; }

    public double Top { get; set; }

    public double Bottom { get; set; }

    public double Over { get; set; }

    public double Under { get; set; }

    public Distances()
    {

    }

    public Distances(
        double left = default,
        double right = default,
        double top = default,
        double bottom = default,
        double over = default,
        double under = default,
        double? horizontal = null,
        double? vertical = null,
        double? deep = null
    )
    {
        Left = left;

        Right = right;

        Top = top;

        Bottom = bottom;

        Over = over;

        Under = under;

        if (horizontal is not null)
        {
            Left = horizontal.Value;
            Right = horizontal.Value;
        }

        if (vertical is not null)
        {
            Top = vertical.Value;
            Bottom = vertical.Value;
        }

        if (deep is not null)
        {
            Over = deep.Value;
            Under = deep.Value;
        }
    }

    public static Distances Parse(string text)
    {
        FormatException Failed() => new($"Can not parse {nameof(Distances)} from text: {text}");

        var regex = @"<type>\((?<g1st>-?\d+(\.\d+)?)(,(?<g2nd>-?\d+(\.\d+)?))?\)";

        var h_regex = regex.Replace("<type>", "h");

        var v_regex = regex.Replace("<type>", "v");

        var d_regex = regex.Replace("<type>", "d");

        var ltrb_regex = @"-?\d+(\.\d+)?,".Repeat(3) + @"-?\d+(\.\d+)?";

        var ltrbou_regex = @"-?\d+(\.\d+)?,".Repeat(5) + @"-?\d+(\.\d+)?";

        var h_match = Regex.Match(text, h_regex);

        if (h_match.Success)
        {
            var first = h_match.Groups["g1st"].Value;
            var second = h_match.Groups["g2nd"].Value;

            if (!second.IsNullOrWhiteSpace())
                return new Distances(
                    left: double.TryParse(first, out var stv) ? stv : throw Failed(),
                    right: double.TryParse(second, out var ndv) ? ndv : throw Failed()
                );
            else return new Distances(
                horizontal: double.TryParse(first, out var stv) ? stv : throw Failed()
            );
        }

        var v_match = Regex.Match(text, v_regex);

        if (v_match.Success)
        {
            var first = v_match.Groups["g1st"].Value;
            var second = v_match.Groups["g2nd"].Value;

            if (!second.IsNullOrWhiteSpace())
                return new Distances(
                    top: double.TryParse(first, out var stv) ? stv : throw Failed(),
                    bottom: double.TryParse(second, out var ndv) ? ndv : throw Failed()
                );
            else return new Distances(
                vertical: double.TryParse(first, out var stv) ? stv : throw Failed()
            );
        }

        var d_match = Regex.Match(text, d_regex);

        if (d_match.Success)
        {
            var first = d_match.Groups["g1st"].Value;
            var second = d_match.Groups["g2nd"].Value;

            if (!second.IsNullOrWhiteSpace())
                return new Distances(
                    over: double.TryParse(first, out var stv) ? stv : throw Failed(),
                    under: double.TryParse(second, out var ndv) ? ndv : throw Failed()
                );
            else return new Distances(
                deep: double.TryParse(first, out var stv) ? stv : throw Failed()
            );
        }

        var ltrbou_match = Regex.Match(text, ltrbou_regex) ?? throw Failed();

        if (ltrbou_match.Success)
        {
            var items = ltrbou_match.Value.Split(',')
                .AssertCount(6, Failed())
                .Select(
                    x => double.TryParse(x, out var i) ? i : throw Failed()
                ).ToList();

            return new Distances(
                items[0],
                items[1],
                items[2],
                items[3],
                items[4],
                items[5]
            );
        }

        var ltrb_match = Regex.Match(text, ltrb_regex) ?? throw Failed();

        if (ltrb_match.Success)
        {
            var items = ltrb_match.Value.Split(',').AssertCount(4, Failed());

            var parsed = items.Select(
                x => double.TryParse(x, out var i) ? i : throw Failed()
            ).ToList();

            return new Distances(parsed[0], parsed[1], parsed[2], parsed[3]);
        }

        throw Failed();
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj) => ToString().Equals(obj.ToString());

    public override string ToString() => $"{Left},{Top},{Right},{Bottom},{Over},{Under}";
}

