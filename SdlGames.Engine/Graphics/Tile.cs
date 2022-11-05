namespace SdlGames.Engine.Graphics;

public class Tile
{
    public Sprite? Sprite { get; init; }
    public SpriteAnimation? SpriteAnimation { get; init; }
    
    public Tile(Sprite sprite)
    {
        this.Sprite = sprite;
        this.SpriteAnimation = null;
    }
    
    public Tile(SpriteAnimation spriteAnimation)
    {
        this.Sprite = null;
        this.SpriteAnimation = spriteAnimation;
    }
}