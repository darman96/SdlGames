using SdlGames.Engine.ECS;
using SdlGames.Engine.ECS.System;
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


    public IWindow Window => this.context;
    public IRenderer Renderer => this.context;
    public ResourceManager ResourceManager => this.resourceManager;

    private readonly SdlContext context;
    private readonly ResourceManager resourceManager = new();
    private readonly ComponentStore componentStore = new();
    private readonly EntityStore entityStore;
    private readonly SystemManager systemManager;
    
    private GameManager(string title, int width, int height)
    {
        this.context = new SdlContext(title, width, height);
        this.entityStore = new EntityStore(this.componentStore);
        this.systemManager = new SystemManager(this.componentStore);
        this.systemManager.AddSystem(new SpriteSystem(this.Renderer));
    }
    
    public static void Initialize(string title, int width, int height)
    {
        instance = new GameManager(title, width, height);
    }
    
    public Entity CreateEntity(params object[] components)
    {
        return this.entityStore.CreateEntity(components);
    }
    
    public Entity? GetEntity(Guid id)
    {
        return this.entityStore.GetEntity(id);
    }

    internal void Update()
    {
        this.systemManager.UpdateSystems();
    }

}