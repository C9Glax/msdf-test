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
render.Save("render.png", ImageFormat.Png);
Console.WriteLine("Done.");