using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Common.BasicHelper.Graphics.Screen;

public class Location(double x = default, double y = default, double z = default)
{
    public double X { get; set; } = x;

    public double Y { get; set; } = y;

    public double Z { get; set; } = z;

    /// <summary>
    /// Parse from [string]
    /// </summary>
    /// <param name="text">[string]</param>
    /// <returns>[Location]</returns>
    /// Format:
    /// "X,Y,Z"
    public static Location Parse(string text)
    {
        var regex = @"-?\d+(\.\d+)?,-?\d+(\.\d+)?(,-?\d+(\.\d+)?)?";

        FormatException Failed() => new($"Can not parse {nameof(Location)} from text: {text}");

        var match = Regex.Match(text, regex) ?? throw Failed();

        if (match.Success)
        {
            var parts = match.Value.Split(',');

            if (parts.Length >= 2)
            {
                double x, y;

                x = double.Parse(parts[0].Trim());
                y = double.Parse(parts[1].Trim());

                double? z = null;

                if (parts.Length == 3)
                    z = double.TryParse(parts[2].Trim(), out var zValue) ? zValue : null;

                var location = new Location()
                {
                    X = x,
                    Y = y,
                    Z = z ?? default,
                };

                return location;
            }
            else throw Failed();
        }
        else throw Failed();
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj) => ToString().Equals(obj.ToString());

    public override string ToString() => $"{X},{Y},{Z}";
}


