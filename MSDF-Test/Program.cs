using System.Drawing;
using System.Drawing.Imaging;
using MSDF_Test;

MSDF msdf = new ("T_RobotoBlack_FONT.png", 66, 3);

Console.Write("String: ");
string? str;
do
{
    str = Console.ReadLine();
}while(str is null);
Console.Write("Size: ");
string? sizeStr;
int size;
do
{
    sizeStr = Console.ReadLine();
}while(sizeStr is null || !int.TryParse(sizeStr, out size));

Console.WriteLine("Rendering...");
Bitmap render = msdf.Render(str, size);
#pragma warning disable CA1416
render.Save("render.png", ImageFormat.Png);
#pragma warning restore CA1416
Console.WriteLine("Done.");