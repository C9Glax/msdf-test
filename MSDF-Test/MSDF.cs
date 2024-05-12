using System.Drawing;

namespace MSDF_Test;

public class MSDF
{
    private readonly Texture _texture;
    private readonly Dictionary<char, Glyph> _glyphs = new();

    public MSDF(string texturePath, int glyphSize, int padding)
    {
        this._texture = new Texture(texturePath, glyphSize, padding);
        List<Bitmap> glyphBitmaps = this._texture.GetGlyphBitmaps();
        for(int i = 32; i < 128; i++)
            _glyphs.Add((char)(i+1), new Glyph(glyphBitmaps[i]));
    }
    
    public Bitmap Render(string str, int size)
    {
        Bitmap ret = new(size * str.Length, size);
        using (Graphics grD = Graphics.FromImage(ret))            
        {
            for(int i = 0; i < str.Length; i++)
            {
                if (_glyphs.TryGetValue(str[i], out Glyph? glyph))
                {
                    int destOffsetX = i * size;
                    grD.DrawImage(glyph.GetBitmap(size, Color.White, Color.Transparent), new PointF(destOffsetX, 0));
                }
            }
        }
        return ret;
    }
}