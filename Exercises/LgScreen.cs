using System.Globalization;
using System.Numerics;

public class LGScreen
{
    Screen[] screens;
    public int ScreenCount;

    public LGScreen()
    {
        string[] lines = File.ReadAllLines("data/products.txt");
        ScreenCount = lines.Length / 4;
        screens = new Screen[ScreenCount];

        for (int i = 0; i < ScreenCount; i++)
        {
            int height = int.Parse(lines[i * 4 + 1]);
            int width = int.Parse(lines[i * 4 + 2]);
            screens[i] = new Screen(height, width);
            Console.WriteLine($"Screen {i + 1}: {height} x {width}");
        }

        BigInteger result = 0;
        foreach (var screen in screens)
        {
            result += screen.microPixels;
        }

        Console.WriteLine("Total microPixels: " + result.ToString(CultureInfo.InvariantCulture));
    }
}

public struct Screen
{
    public int height;
    public int width;

    public BigInteger microPixels;

    public Screen(int height, int width)
    {
        this.height = height;
        this.width = width;

        BigInteger area = new BigInteger(height) * new BigInteger(width);
        int exponent = (int)Math.Round((double)Math.Max(height, width) / Math.Min(height, width));

        microPixels = BigInteger.Pow(area, exponent);
    }
}