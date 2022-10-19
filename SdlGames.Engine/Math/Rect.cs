namespace SdlGames.Engine.Math;

public class RectF
{
    public Vector2F Position { get; set; }
    public Vector2F Size { get; set; }

    public RectF(Vector2F position, Vector2F size)
    {
        this.Position = position;
        this.Size = size;
    }

    public RectF(float x, float y, float width, float height)
        : this(new Vector2F(x, y), new Vector2F(width, height)) { }
}

public class RectD
{
    public Vector2D Position { get; set; }
    public Vector2D Size { get; set; }

    public RectD(Vector2D position, Vector2D size)
    {
        this.Position = position;
        this.Size = size;
    }

    public RectD(double x, double y, double width, double height)
        : this(new Vector2D(x, y), new Vector2D(width, height)) { }
}

public class RectI
{
    public Vector2I Position { get; set; }
    public Vector2I Size { get; set; }

    public RectI(Vector2I position, Vector2I size)
    {
        this.Position = position;
        this.Size = size;
    }

    public RectI(int x, int y, int width, int height)
        : this(new Vector2I(x, y), new Vector2I(width, height)) { }
}