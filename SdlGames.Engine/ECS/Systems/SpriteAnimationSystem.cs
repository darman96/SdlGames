using SdlGames.Engine.ECS.Components;
using SdlGames.Engine.Interfaces;

namespace SdlGames.Engine.ECS.Systems;

public class SpriteAnimationSystem
{
    private readonly IRenderer renderer;
    private readonly GameTimeManager gameTimeManager;

    public SpriteAnimationSystem(IRenderer renderer, GameTimeManager gameTimeManager)
    {
        this.renderer = renderer;
        this.gameTimeManager = gameTimeManager;
    }

    public void Update(TransformComponent transformComponent, SpriteAnimationComponent spriteAnimationComponent)
    {
        if (spriteAnimationComponent.CurrentFrameTime > spriteAnimationComponent.FrameTime)
        {
            spriteAnimationComponent.CurrentFrame = spriteAnimationComponent.Loop
                ? (spriteAnimationComponent.CurrentFrame + 1) % spriteAnimationComponent.Animation.Length
                : Math.Clamp(spriteAnimationComponent.CurrentFrame + 1, 0,
                    spriteAnimationComponent.Animation.Length - 1);

            spriteAnimationComponent.CurrentFrameTime = 0f;
        }
        
        this.renderer.DrawSprite(
            transformComponent.Position,
            spriteAnimationComponent.Animation.Frames[spriteAnimationComponent.CurrentFrame]);
        
        spriteAnimationComponent.CurrentFrameTime += this.gameTimeManager.DeltaTime;
    }
}