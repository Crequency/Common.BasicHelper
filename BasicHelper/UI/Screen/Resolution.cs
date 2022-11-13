using System;
using System.Collections.Generic;

namespace BasicHelper.UI.Screen
{
    public class Resolution
    {
        internal double? Width { get; set; }

        internal double? Height { get; set; }

        internal double? FramePerSecond { get; set; }

        internal double? Area => Width * Height;

        internal double? AspectRatio => Width / Height;

        internal string Description { get; set; } = string.Empty;

        /// <summary>
        /// 宽高整数化
        /// </summary>
        public Resolution Integerization()
        {
            if (Width != null)
                Width = System.Math.Round(Width.Value, 0);
            if (Height != null)
                Height = System.Math.Round(Height.Value, 0);
            return this;
        }

        /// <summary>
        /// 重载运算符: 除法
        /// </summary>
        /// <param name="a">分辨率1</param>
        /// <param name="b">分辨率2</param>
        /// <returns>面积之比</returns>
        public static double? operator /(Resolution a, Resolution b) => a.Area / b.Area;

        /// <summary>
        /// 重载运算符: 加法
        /// </summary>
        /// <param name="a">分辨率1</param>
        /// <param name="b">分辨率2</param>
        /// <returns>分辨率</returns>
        public static Resolution operator +(Resolution a, Resolution b) => new Resolution
        {
            Width = a.Width + b.Width,
            Height = a.Height + b.Height,
            FramePerSecond = a.FramePerSecond + b.FramePerSecond,
            Description = $"{a.Description}\nPlus\n{b.Description}"
        };

        /// <summary>
        /// 重写 ToString() 方法
        /// </summary>
        /// <returns>表示分辨率及刷新率的字符串</returns>
        public override string ToString()
        {
            return $"{Width}x{Height}@{FramePerSecond}";
        }

        /// <summary>
        /// 重写 Equals() 方法
        /// </summary>
        /// <param name="obj">目标比较分辨率</param>
        /// <returns>是否相等</returns>
        public override bool Equals(object obj)
        {
            Resolution res = obj as Resolution;
            if (Width.Equals(res.Width) && Height.Equals(res.Height))
                if (FramePerSecond != null && res.FramePerSecond != null)
                    if (FramePerSecond.Equals(res.FramePerSecond))
                        return true;
                    else return false;
                else return true;
            else return false;
        }

        /// <summary>
        /// 重写 GetHashCode() 方法
        /// </summary>
        /// <returns>哈希值</returns>
        public override int GetHashCode() => (int)(0
            + (Area.GetHashCode() ^ AspectRatio.GetHashCode())
            + (Width.GetHashCode() ^ Height.GetHashCode())
            + FramePerSecond == null ? 0 : FramePerSecond);

        /// <summary>
        /// 根据字符串返回分辨率对象
        /// </summary>
        /// <param name="input">字符串: 宽x高@刷新率</param>
        /// <returns>分辨率对象</returns>
        public static Resolution Parse(string input)
        {
            string[] res_fps = input.Split('@');
            string[] res = res_fps[0].Split('x');
            Resolution resolution = new Resolution
            {
                Width = Convert.ToDouble(res[0]),
                Height = Convert.ToDouble(res[1])
            };
            if (res_fps.Length == 2) resolution.FramePerSecond = Convert.ToDouble(res_fps[1]);
            else resolution.FramePerSecond = null;
            return resolution;
        }

        /// <summary>
        /// 根据字符串返回分辨率对象
        /// </summary>
        /// <param name="input">字符串: 宽x高@刷新率</param>
        /// <param name="descr">字符串: 描述信息</param>
        /// <returns>分辨率对象</returns>
        public static Resolution Parse(string input, string descr)
        {
            string[] res_fps = input.Split('@');
            string[] res = res_fps[0].Split('x');
            Resolution resolution = new Resolution
            {
                Width = Convert.ToDouble(res[0]),
                Height = Convert.ToDouble(res[1]),
                Description = descr
            };
            if (res_fps.Length == 2) resolution.FramePerSecond = Convert.ToDouble(res_fps[1]);
            else resolution.FramePerSecond = null;
            return resolution;
        }

        public static readonly List<Resolution> resolutions = new List<Resolution>()
        {
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
        };

        /// <summary>
        /// 建议分辨率
        /// </summary>
        /// <param name="screen">理想屏幕</param>
        /// <param name="content">理想内容</param>
        /// <param name="actual">实际屏幕</param>
        /// <returns>建议的内容尺寸</returns>
        public static Resolution Suggest(Resolution screen, Resolution content, Resolution actual)
        {
            //  理想内容面积与理想屏幕面积之比
            double? areaper = content / screen;

            //  理想内容面积     真实内容面积
            //  ----------  =  -----------
            //  理想屏幕面积     真实屏幕面积

            //  真实内容面积 = 理想内容面积 / 理想屏幕面积 * 真实屏幕面积

            //  真实内容面积
            double? tararea = areaper * actual.Area;

            //  理想尺寸: x, y
            //  x / y = 理想内容宽高比 = 真实内容宽高比 = content.AspectRatio
            //  x * y = 真实内容面积 = tararea
            //  联立, 得: y**2 = tararea / content.AspectRatio
            //           x = content.AspectRatio * y

            Resolution suggest = new Resolution();

            double? yy = tararea / content.AspectRatio;
            if (yy != null)
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
    }
}
