using System.Numerics;

namespace SdlGames.Engine.Math;

public class Rect
{
    public float X => this.Position.X;
    public float Y => this.Position.Y;
    public Vector2 Position { get; set; }

    public float Width => this.Size.X;
    public float Height => this.Size.Y;
    public Vector2 Size { get; set; }

    public Rect(Vector2 position, Vector2 size)
    {
        this.Position = position;
        this.Size = size;
    }

    public Rect(float x, float y, float width, float height)
        : this(new Vector2(x, y), new Vector2(width, height)) { }
}
