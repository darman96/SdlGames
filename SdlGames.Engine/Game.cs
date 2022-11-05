using System.Collections.Immutable;
using SdlGames.Engine.ECS.Component;
using SdlGames.Engine.ECS.Entity;
using SdlGames.Engine.ECS.System;
using SdlGames.Engine.Event;
using SdlGames.Engine.Interfaces;
using EventHandler = SdlGames.Engine.Event.EventHandler;

namespace SdlGames.Engine;

public abstract partial class Game
{
    public event EventHandler OnEvent;
    
    protected IWindow Window { get; init; }
    protected IRenderer Renderer { get; init; }
    protected ResourceManager ResourceManager => this.resourceManager;

    private bool isRunning;
    private GameTimeManager gameTimeManager;
    private ResourceManager resourceManager;
    private EntityStore entityStore;
    private ComponentStore componentStore;
    private SystemManager systemManager;

    public Entity CreateEntity(params object[] components)
        => this.entityStore.CreateEntity(components);

    public Entity? GetEntity(Guid id)
        => this.entityStore.GetEntity(id);

    public ImmutableArray<Entity> GetEntities()
        => this.entityStore.GetEntities();

    public void AddSystem(object system)
        => this.systemManager.AddSystem(system);

    public void Run()
    {
        this.InitializeInt();
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
            this.UpdateInt();
            this.Update();
            this.Renderer.Present();
        }
    }

    public abstract void Initialize();
    public abstract void Update();
    
    public virtual void HandleEvent(EventType eventType) { }

    private void UpdateInt()
    {
        this.gameTimeManager.Update();
        this.systemManager.UpdateSystems();
    }
    
    private void InitializeInt()
    {
        this.gameTimeManager = new GameTimeManager();
        this.resourceManager = new ResourceManager(this.Renderer);
        this.componentStore = new ComponentStore();
        this.entityStore = new EntityStore(this.componentStore);
        this.systemManager = new SystemManager(this.componentStore);
        this.systemManager.AddSystem(new SpriteSystem(this.Renderer));
        this.systemManager.AddSystem(new SpriteAnimationSystem(this.Renderer, this.gameTimeManager));
        
        this.OnEvent += this.HandleEvent;
        this.isRunning = true;
    }
}