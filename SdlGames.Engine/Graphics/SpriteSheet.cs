using System.Drawing;

namespace SdlGames.Engine.Graphics;

public class SpriteSheet
{
    private readonly Texture texture;
    private readonly Size size;

    public SpriteSheet(Texture texture, Size size)
    {
        this.texture = texture;
        this.size = size;
    }

    public Sprite GetSprite(uint index)
    {
        var tilePosition = new Point(
            this.size.Width * (int)(index % (this.texture.Width / this.size.Width)),
            this.size.Height * (int)(index / (this.texture.Width / this.size.Width)));
        var sourceRect = new RectangleF(tilePosition, this.size);
        
        return new Sprite(this.texture, sourceRect);
    }
    
    public Sprite[] GetSprites(uint start, uint count)
    {
        var sprites = new Sprite[count];
        for (var i = 0; i < count; i++)
        {
            sprites[i] = this.GetSprite(start + (uint)i);
        }

        return sprites;
    }
    
    public SpriteAnimation GetSpriteAnimation(uint start, uint frameCount, int framesPerSecond)
    {
        return new SpriteAnimation(this.GetSprites(start, frameCount), framesPerSecond);
    }
}