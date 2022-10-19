namespace SdlGames.Engine;

public struct Color
{
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }
    public int A { get; set; }

    public static Color Black()
        => new Color(0, 0, 0, 255);
    
    public static Color White()
        => new Color(255, 255, 255);

    public static Color Red()
        => new Color(255, 0, 0);
    
    public Color(int r, int g, int b, int a = 255)
    {
        this.R = r;
        this.G = g;
        this.B = b;
        this.A = a;
    }
}