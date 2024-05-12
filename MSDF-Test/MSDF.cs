using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace MSDF_Test;

// ReSharper disable once InconsistentNaming
[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
public class MSDF
{
    private readonly Dictionary<char, Glyph> _glyphs = new();

    public MSDF(string texturePath, int glyphSize, int padding)
    {
        Texture texture = new (texturePath, glyphSize, padding);
        List<Bitmap> glyphBitmaps = texture.GetGlyphBitmaps();
        for(int i = 32; i < 128; i++)
            _glyphs.Add((char)(i+1), new Glyph(glyphBitmaps[i]));
    }
    
    public Bitmap Render(string str, int size)
    {
        Bitmap ret = new(size * str.Length, size);
        using Graphics grD = Graphics.FromImage(ret);
        for(int i = 0; i < str.Length; i++)
        {
            if (_glyphs.TryGetValue(str[i], out Glyph? glyph))
            {
                int destOffsetX = i * size;
                grD.DrawImage(glyph.GetBitmap(size, Color.White, Color.Transparent), new PointF(destOffsetX, 0));
            }
        }

        return ret;
    }
}