using SdlGames.Engine.Event;

namespace SdlGames.Engine;

public abstract class Game
{
    private bool running = true;
    private GameManager gameManager;
    
    protected Game(string title, int width, int height)
    {
        GameManager.Initialize(title, width, height);
        this.gameManager = GameManager.Instance;
    }

    public void Run()
    {
        this.Initialize();
        var window = this.gameManager.Window;
        while (this.running)
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
            window.Clear(Color.Black());
            this.gameManager.Update();
            
            window.Present();
        }
    }

    public abstract void Initialize();
}