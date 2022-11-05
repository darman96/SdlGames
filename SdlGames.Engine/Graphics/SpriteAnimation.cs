namespace SdlGames.Engine.Graphics;

public class SpriteAnimation
{
    public Sprite[] Frames { get; private set; }
    public int FramesPerSecond { get; private set; }
    public int Length => this.Frames.Length;
    
    public SpriteAnimation(Sprite[] frames, int framesPerSecond)
    {
        this.Frames = frames;
        this.FramesPerSecond = framesPerSecond;
    }
}