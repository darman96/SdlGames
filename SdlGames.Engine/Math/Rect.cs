using System.Numerics;

namespace SdlGames.Engine.Math;

public class Rect
{
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }

    public Rect(Vector2 position, Vector2 size)
    {
        this.Position = position;
        this.Size = size;
    }

    public Rect(float x, float y, float width, float height)
        : this(new Vector2(x, y), new Vector2(width, height)) { }
}
