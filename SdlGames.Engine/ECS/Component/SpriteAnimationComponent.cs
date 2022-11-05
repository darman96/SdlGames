using SdlGames.Engine.Graphics;

namespace SdlGames.Engine.ECS.Component;

public class SpriteAnimationComponent
{
    public SpriteAnimation Animation;
    public int CurrentFrame;
    public float CurrentFrameTime;
    public float FrameTime;
    public bool Loop;
    
    public SpriteAnimationComponent(SpriteAnimation animation, bool loop)
    {
        this.Animation = animation;
        this.FrameTime = 1f / animation.FramesPerSecond;
        this.CurrentFrameTime = 0f;
        this.Loop = loop;
    }
}