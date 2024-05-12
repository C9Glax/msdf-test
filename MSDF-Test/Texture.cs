using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace MSDF_Test;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
internal readonly struct Texture
{
    private readonly Bitmap _image;
    private readonly int _glyphSize;
    private readonly int _padding;

    internal Texture(Bitmap image, int glyphSize, int padding)
    {
        this._image = image;
        this._glyphSize = glyphSize;
        this._padding = padding;
    }

    internal List<Bitmap> GetGlyphBitmaps()
    {
        List<Bitmap> ret = new();
        for (int y = _padding; y < _image.Width - _padding; y += _glyphSize + _padding)
        {
            for (int x = _padding; x < _image.Height - _padding; x += _glyphSize + _padding)
            {
                Point topLeft = new (x, y);
                ret.Add(_image.Clone(new Rectangle(topLeft, new Size(_glyphSize, _glyphSize)), _image.PixelFormat));
            }
        }

        return ret;
    }

    public Texture(string imagePath, int glyphSize, int padding) : this((Bitmap)Image.FromFile(imagePath), glyphSize, padding)
    {
        
    }
}