using SdlGames.Engine.Graphics;

namespace SdlGames.Engine.ECS.Components;

public class SpriteComponent
{
    public Sprite Sprite;

    public SpriteComponent(Sprite sprite)
    {
        this.Sprite = sprite;
    }
}