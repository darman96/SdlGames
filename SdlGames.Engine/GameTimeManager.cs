namespace SdlGames.Engine;

public class GameTimeManager
{
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
                _ => elapsedSeconds
            };
            this.lastTick = currentTick;
        }
        Thread.Sleep(1);
    }
}