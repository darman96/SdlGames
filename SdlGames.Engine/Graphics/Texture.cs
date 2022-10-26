using System.Numerics;

namespace SdlGames.Engine.Graphics;

public class Texture
{
    public float Width => Size.X;
    public float Height => Size.Y;
    public Vector2 Size { get; private set; }

    internal IntPtr Handle => this.textureHandle;
    private readonly IntPtr textureHandle;

    internal Texture(IntPtr textureHandle, Vector2 size)
    {
        this.textureHandle = textureHandle;
        this.Size = size;
    }
}