using SdlGames.Engine.ECS;
using SdlGames.Engine.ECS.Systems;
using SdlGames.Engine.Extensions;
using SdlGames.Engine.Internal;
using SdlGames.Engine.Internal.Interfaces;

namespace SdlGames.Engine;

public class GameManager
{
    public static GameManager Instance
    {
        get
        {
            if (instance is null)
                throw new Exception("GameManager not initialized");

            return instance;
        }
    }
    private static GameManager? instance;
    

    public IWindow Window { get; private set; }
    public IRenderer Renderer { get; private set; }
    public ResourceManager ResourceManager { get; private set; }

    private readonly ComponentStore componentStore = new();
    
    private GameManager(string title, int width, int height)
    {
        var context = new SdlContext(title, width, height);
        this.Window = context;
        this.Renderer = context;
        this.ResourceManager = new ResourceManager();
    }
    
    public static void Initialize(string title, int width, int height)
    {
        instance = new GameManager(title, width, height);
    }
    
    internal void Update()
    {
    }

}