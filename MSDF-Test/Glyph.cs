﻿using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace MSDF_Test;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
internal class Glyph
{
    private readonly Bitmap _originalBitmap;
    private const float MaxColor = 255; //255 for Bitmaps
    private const float DistanceRange = 255; //255 for Bitmaps

    public Glyph(Bitmap texture)
    {
        this._originalBitmap = texture;
    }

    internal Bitmap GetBitmap(int size, Color foreground, Color background)
    {
        Bitmap ret = new(size, size, _originalBitmap.PixelFormat);
        for (int x = 0; x < ret.Width; x++)
        {
            for (int y = 0; y < ret.Height; y++)
            {
                PointF scaledPoint = new (x, y);
                ret.SetPixel(x, y, GeneratePixel(scaledPoint.Scale((float)_originalBitmap.Width / size)) ? foreground : background);
            }
        }

        return ret;
    }

    private bool GeneratePixel(PointF point)
    {
        Color s = SampleBilinear(point);
        float sample = Median(s);
        float d = ColorDistance(sample);
        return d >= 0;
    }

    private Color SampleBilinear(PointF point)
    {
        int x1 = (int)Math.Floor(point.X - .5);
        int y1 = (int)Math.Floor(point.Y - .5);
        int x2 = x1 + 1;
        int y2 = y1 + 1;
        double wx = point.X - x1 - .5;
        double wy = point.Y - y1 - .5;
        
        if (x1 >= 0 && y1 >= 0 && x2 <= (_originalBitmap.Width - 1) && y2 <= (_originalBitmap.Height - 1))
        {
            Color x1y1 = _originalBitmap.GetPixel(x1, y1).MultiplyWith((1 - wx) * (1 - wy));
            Color x1y2 = _originalBitmap.GetPixel(x1, y2).MultiplyWith((1 - wx) * wy);
            Color x2y1 = _originalBitmap.GetPixel(x2, y1).MultiplyWith(wx * (1 - wy));
            Color x2y2 = _originalBitmap.GetPixel(x2, y2).MultiplyWith(wx * wy);

            return x1y1.Add(x1y2).Add(x2y1).Add(x2y2);
        }

        return Color.Black; //Edges can not be interpolated. Just return 0
    }

    private static float Median(float r, float g, float b)
    {
        return Math.Max(Math.Min(r, g), Math.Min(Math.Max(r, g), b));
    }

    private static float Median(Color c) => Median(c.R, c.G, c.B);

    private static float ColorDistance(float sample)
    {
        return ((sample / MaxColor) - .5f) * DistanceRange;
    }
}