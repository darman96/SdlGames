using SdlGames.Engine.ECS.Component;
using SdlGames.Engine.Internal.Interfaces;

namespace SdlGames.Engine.ECS.System;

public struct SpriteSystemComponents
{
    public TransformComponent Transform;
    public SpriteComponent Sprite;
}

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
            spriteComponent.Texture,
            spriteComponent.SourceRect);
    }
}