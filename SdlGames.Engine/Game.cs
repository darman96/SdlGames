using SdlGames.Engine.Event;
using SdlGames.Engine.Internal;
using SdlGames.Engine.Internal.Interfaces;

namespace SdlGames.Engine;

public abstract class Game
{
    private bool running = true;
    
    protected readonly IWindow window;

    protected Game(string title, int width, int height)
    {
        this.window = new SdlContext(title, width, height);
    }

    public void Run()
    {
        while (running)
        {
            window.PollEvents(type =>
            {
                switch (type)
                {
                    case EventType.None:
                        break;
                    case EventType.Quit:
                        this.running = false;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            });
            this.OnUpdate();
        }
    }

    public abstract void OnUpdate();
}