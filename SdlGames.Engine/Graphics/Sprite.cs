using System.Drawing;

namespace SdlGames.Engine.Graphics;

public class Sprite
{
    internal Texture Texture => this.spriteTexture;
    internal RectangleF SourceRect => this.sourceRect;
    
    private readonly Texture spriteTexture;
    private readonly RectangleF sourceRect;

    internal Sprite(Texture texture, RectangleF sourceRect)
    {
        this.spriteTexture = texture;
        this.sourceRect = sourceRect;
    }
}