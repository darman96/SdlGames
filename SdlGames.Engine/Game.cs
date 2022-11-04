using SdlGames.Engine.Event;
using SdlGames.Engine.Interfaces;
using EventHandler = SdlGames.Engine.Event.EventHandler;

namespace SdlGames.Engine;

public abstract partial class Game
{
    public event EventHandler OnEvent;
    
    protected IWindow Window { get; init; }
    protected IRenderer Renderer { get; init; }

    private bool isRunning;

    public void Run()
    {
        this.isRunning = true;
        this.OnEvent += this.HandleEvent;
        
        this.Initialize();
        while (this.isRunning)
        {
            this.Window.PollEvents(type =>
            {
                switch (type)
                {
                    case EventType.None:
                        break;
                    case EventType.Quit:
                        this.isRunning = false;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
                
                this.OnEvent.Invoke(type);
            });
            this.Renderer.Clear(Color.Black());
            this.Renderer.Present();
        }
    }

    public abstract void Initialize();
    
    public virtual void HandleEvent(EventType eventType) { }
}