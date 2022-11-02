using SdlGames.Engine.Graphics;

namespace SdlGames.Engine.ECS.Component;

public class SpriteComponent
{
    public Sprite Sprite;

    public SpriteComponent(Sprite sprite)
    {
        this.Sprite = sprite;
    }
}