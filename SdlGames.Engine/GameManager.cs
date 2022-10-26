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


    public IWindow Window => context;
    public IRenderer Renderer => context;
    public ResourceManager ResourceManager => resourceManager;

    private readonly SdlContext context;
    private readonly ResourceManager resourceManager = new();
    private readonly ComponentStore componentStore = new();
    private readonly EntityStore entityStore;
    
    private GameManager(string title, int width, int height)
    {
        this.context = new SdlContext(title, width, height);
        this.entityStore = new EntityStore(componentStore);
    }
    
    public static void Initialize(string title, int width, int height)
    {
        instance = new GameManager(title, width, height);
    }
    
    public Entity CreateEntity(params object[] components)
    {
        return entityStore.CreateEntity(components);
    }
    
    public Entity? GetEntity(Guid id)
    {
        return entityStore.GetEntity(id);
    }

    internal void Update()
    {
        var updateMethod = typeof(SpriteSystem)
            .GetMethod("Update");
        var parameters = updateMethod.GetParameters();
    }

}