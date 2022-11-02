using SdlGames.Engine.ECS.Component;
using SdlGames.Engine.Internal.Interfaces;

namespace SdlGames.Engine.ECS.System;

public class SpriteAnimationSystem
{
    private readonly IRenderer renderer;

    public SpriteAnimationSystem(IRenderer renderer)
    {
        this.renderer = renderer;
    }

    public void Update(TransformComponent transformComponent, SpriteAnimationComponent spriteAnimationComponent)
    {
        if (spriteAnimationComponent.CurrentFrameTime > spriteAnimationComponent.FrameTime)
        {
            spriteAnimationComponent.CurrentFrame = (spriteAnimationComponent.CurrentFrame + 1) 
                                                    % spriteAnimationComponent.Frames.Length;

            spriteAnimationComponent.CurrentFrameTime = 0f;
        }
        
        var sprite = spriteAnimationComponent.Frames[spriteAnimationComponent.CurrentFrame];
        this.renderer.DrawSprite(
            transformComponent.Position,
            sprite.Texture,
            sprite.SourceRect);
        
        spriteAnimationComponent.CurrentFrameTime += GameManager.DeltaTime;
    }
}