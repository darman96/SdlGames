using SdlGames.Engine.ECS.Components;
using SdlGames.Engine.Interfaces;

namespace SdlGames.Engine.ECS.Systems;

public class SpriteSystem
{
    private readonly IRenderer renderer;

    public SpriteSystem(IRenderer renderer)
    {
        this.renderer = renderer;
    }
    
    public void Update(TransformComponent transformComponent, SpriteComponent spriteComponent)
    {
        this.renderer.DrawSprite(
            transformComponent.Position,
            spriteComponent.Sprite);
    }
}