using SdlGames.Engine.ECS.Component;

namespace SdlGames.Engine.ECS.Entity;

public struct Entity
{
    public Guid Id { get; init; }

    private readonly ComponentStore componentStore;
    
    internal Entity(Guid id, ComponentStore componentStore)
    {
        this.Id = id;
        this.componentStore = componentStore;
    }
    
    public bool HasComponent<T>() where T : notnull
        => this.componentStore.HasComponent<T>(this.Id);
    
    public T GetComponent<T>() where T : notnull
        => this.componentStore.GetComponent<T>(this.Id);
    
    public void AddComponent<T>(T component) where T : notnull
        => this.componentStore.RegisterComponent(this.Id, component);
}