namespace SdlGames.Engine.ECS;

public struct Entity
{
    public Guid Id { get; init; }

    private readonly ComponentStore componentStore;
    
    internal Entity(Guid id, ComponentStore componentStore)
    {
        this.Id = id;
        this.componentStore = componentStore;
    }
    
    public bool HasComponent<T>() where T : struct
        => this.componentStore.HasComponent<T>(this.Id);
    
    public T GetComponent<T>() where T : struct
        => this.componentStore.GetComponent<T>(this.Id);
    
    public void AddComponent<T>(T component) where T : struct
        => this.componentStore.RegisterComponent(this.Id, component);
}