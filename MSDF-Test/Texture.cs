using System.Drawing;

namespace MSDF_Test;

public struct Texture
{
    internal Bitmap Image;
    internal int GlyphSize;
    internal int Padding;

    public Texture(Bitmap image, int glyphSize, int padding)
    {
        this.Image = image;
        this.GlyphSize = glyphSize;
        this.Padding = padding;
    }

    internal List<Bitmap> GetGlyphBitmaps()
    {
        List<Bitmap> ret = new();
        for (int y = Padding; y < Image.Width - Padding; y += GlyphSize + Padding)
        {
            for (int x = Padding; x < Image.Height - Padding; x += GlyphSize + Padding)
            {
                Point topLeft = new (x, y);
                ret.Add(Image.Clone(new Rectangle(topLeft, new Size(GlyphSize, GlyphSize)), Image.PixelFormat));
            }
        }

        return ret;
    }

    public Texture(string imagePath, int glyphSize, int padding) : this((Bitmap)Bitmap.FromFile(imagePath), glyphSize, padding)
    {
        
    }
}