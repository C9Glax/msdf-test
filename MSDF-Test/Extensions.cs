using System.Drawing;

namespace MSDF_Test;

public static class Extensions
{

    internal static Color MultiplyWith(this Color color, double factor)
    {
        return Color.FromArgb((int)(color.R * factor), (int)(color.G * factor), (int)(color.B * factor));
    }

    internal static Color Add(this Color c1, Color c2)
    {
        return Color.FromArgb(c1.R + c2.R, c1.G + c2.G, c1.B + c2.B);
    }

    internal static PointF Scale(this PointF point, float factor)
    {
        return new PointF(point.X * factor, point.Y * factor);
    }
}