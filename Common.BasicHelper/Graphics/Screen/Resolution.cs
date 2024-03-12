using System;
using System.Collections.Generic;
using System.Linq;
using Common.BasicHelper.Core.Exceptions;
using Common.BasicHelper.Utils.Extensions;

namespace Common.BasicHelper.Graphics.Screen;

public class Resolution
{
    public static readonly List<Resolution> resolutions =
    [
        Parse("800x600", "SVGA"),
        Parse("1024x768", "XGA"),
        Parse("1280x720", "HD"),
        Parse("1280x768", "WXGA"),
        Parse("1280x800", "WXGA"),
        Parse("1280x1024", "SXGA"),
        Parse("1366x768", "WXSGA+"),
        Parse("1440x900", "WXGA+"),
        Parse("1600x1024", "WSXGA"),
        Parse("1680x1050", "WSXGA+"),
        Parse("1920x1080", "Full HD"),
        Parse("1920x1200", "WUXGA"),
        Parse("2048x1080", "2K Resolution"),
        Parse("2048x1536", "QXGA"),
        Parse("2560x1080", "QHD"),
        Parse("2560x1440", "WQHD"),
        Parse("2560x1600", "WQXGA"),
        Parse("2560x2048", "QSXGA"),
        Parse("3200x2048", "WQSXGA"),
        Parse("3200x2400", "QUXGA"),
        Parse("3440x1440", "Ulrea-Wide QHD"),
        Parse("3840x2160", "4K UHD"),
        Parse("3840x2400", "WQUXGA"),
        Parse("4096x2160", "DCI 4K"),
        Parse("5120x4096", "HSXGA"),
        Parse("6400x4096", "WHSXGA"),
        Parse("6400x4800", "HUXGA"),
        Parse("7680x4320", "8K Ultra HD"),
        Parse("7680x4800", "WHUXGA"),
    ];

    public double? Width { get; set; }

    public double? Height { get; set; }

    public double? FramePerSecond { get; set; }

    public double? Area => Width * Height;

    public double? AspectRatio => Width / Height;

    public string? Description { get; set; }

    public Resolution()
    {

    }

    public Resolution(double width = default, double height = default)
    {
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Integerize width and height
    /// </summary>
    public Resolution Integerization()
    {
        if (Width is not null)
            Width = System.Math.Round(Width.Value, 0);
        if (Height is not null)
            Height = System.Math.Round(Height.Value, 0);
        return this;
    }

    /// <summary>
    /// Returns a resolution object based on a string
    /// </summary>
    /// <param name="input">Format: {Width}x{Height}@{FPS}</param>
    /// <returns>Resolution object</returns>
    public static Resolution Parse(string input)
    {
        var res_fps = input.Split('@');
        var res = res_fps[0].Split('x');

        var resolution = new Resolution
        {
            Width = Convert.ToDouble(res[0]),
            Height = Convert.ToDouble(res[1]),
        };

        if (res_fps.Length == 2) resolution.FramePerSecond = Convert.ToDouble(res_fps[1]);
        else resolution.FramePerSecond = null;

        return resolution;
    }

    /// <summary>
    /// Returns a resolution object based on a string
    /// </summary>
    /// <param name="input">Format: {Width}x{Height}@{FPS}</param>
    /// <param name="descr">Description</param>
    /// <returns>Resolution object</returns>
    public static Resolution Parse(string input, string? descr = null)
    {
        var resolution = Parse(input);

        if (descr is null)
            resolution.Description = resolutions.FirstOrDefault((x) => x.Equals(resolution))?.Description;
        else
            resolution.Description = descr;

        return resolution;
    }

    /// <summary>
    /// Suggest content resolution
    /// </summary>
    /// <param name="screen">Target screen resolution</param>
    /// <param name="content">Target content resolution</param>
    /// <param name="actual">Actual screen resolution</param>
    /// <returns>Suggested content resolution</returns>
    public static Resolution Suggest(Resolution screen, Resolution content, Resolution actual)
    {
        //  理想内容面积与理想屏幕面积之比
        var areaper = content / screen;

        //  理想内容面积     真实内容面积
        //  ----------  =  -----------
        //  理想屏幕面积     真实屏幕面积

        //  真实内容面积 = 理想内容面积 / 理想屏幕面积 * 真实屏幕面积

        //  真实内容面积
        var tararea = areaper * actual.Area;

        //  理想尺寸: x, y
        //  x / y = 理想内容宽高比 = 真实内容宽高比 = content.AspectRatio
        //  x * y = 真实内容面积 = tararea
        //  联立, 得: y**2 = tararea / content.AspectRatio
        //           x = content.AspectRatio * y

        var suggest = new Resolution();

        var yy = tararea / content.AspectRatio;

        if (yy is not null)
        {
            suggest.Height = System.Math.Sqrt((double)yy);
            suggest.Width = content.AspectRatio * suggest.Height;
        }
        else
        {
            return content;
        }

        return suggest;
    }

    public static double? operator /(Resolution a, Resolution b) => a.Area / b.Area;

    public static Resolution operator +(Resolution a, Resolution b) => new()
    {
        Width = a.Width + b.Width,
        Height = a.Height + b.Height,
        FramePerSecond = a.FramePerSecond + b.FramePerSecond,
        Description = $"{a.Description}\nPlus\n{b.Description}"
    };

    public override string ToString() =>
        $"{Width}x{Height}{(FramePerSecond is null ? "" : "@")}{FramePerSecond}";

    public override bool Equals(object obj)
    {
        if (obj is not Resolution)
            ErrorCodes.CB0017.BuildMessage(parameterName: nameof(Equals)).Throw<ArgumentException>();

        var target = obj as Resolution;

        return Width == target?.Width && Height == target?.Height && FramePerSecond == target?.FramePerSecond;
    }

    public override int GetHashCode() => (int)(
        0
        + (Area.GetHashCode() ^ AspectRatio.GetHashCode())
        + (Width.GetHashCode() ^ Height.GetHashCode())
        + FramePerSecond ?? 0
        );
}
