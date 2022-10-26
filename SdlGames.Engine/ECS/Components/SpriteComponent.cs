using System.Numerics;
using SdlGames.Engine.Graphics;
using SdlGames.Engine.Math;

namespace SdlGames.Engine.ECS.Components;

public struct SpriteComponent
{
    public Rect SourceRect { get; set; }
    public Texture Texture { get; set; }

    public SpriteComponent(Texture texture, Vector2 tileSize, uint tileIndex)
    {
        this.Texture = texture;
        var cols = (int)texture.Width / (int)tileSize.X;
        this.SourceRect = new Rect(
            tileSize.X * (tileIndex % (cols)),
            tileSize.Y * (tileIndex / (cols)),
            tileSize.X,
            tileSize.Y);
    }
}