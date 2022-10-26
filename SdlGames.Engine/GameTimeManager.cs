namespace SdlGames.Engine;

public class GameTimeManager
{
    private const float DeltaLowLimit = 0.0167f;
    private const float DeltaHighLimit = 0.1f;
    
    private long lastTick;

    public float DeltaTime { get; private set; }
    
    internal void Update()
    {
        long currentTick = DateTime.Now.Ticks;
        if (this.lastTick == 0)
            this.lastTick = currentTick;
        else
        {
            long elapsedTicks = currentTick - this.lastTick;
            float elapsedSeconds = elapsedTicks / (float)TimeSpan.TicksPerSecond;
            this.DeltaTime = elapsedSeconds switch
            {
                < DeltaLowLimit => DeltaLowLimit,
                > DeltaHighLimit => DeltaHighLimit,
                _ => elapsedSeconds
            };
            this.lastTick = currentTick;
        }
    }
}