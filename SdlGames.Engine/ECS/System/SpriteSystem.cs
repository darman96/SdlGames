using SdlGames.Engine.ECS.Component;
using SdlGames.Engine.Interfaces;

namespace SdlGames.Engine.ECS.System;

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