using SdlGames.Engine.Graphics;

namespace SdlGames.Engine.ECS.Component;

public class SpriteAnimationComponent
{
    public Sprite[] Frames;
    public int CurrentFrame;
    public float CurrentFrameTime;
    public float FrameTime;
    public bool Loop;
    
    public SpriteAnimationComponent(Sprite[] frames, int framesPerSecond, bool loop)
    {
        this.Frames = frames;
        this.FrameTime = 1f / framesPerSecond;
        this.CurrentFrameTime = 0f;
        this.Loop = loop;
    }
}