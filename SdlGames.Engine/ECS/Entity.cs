namespace SdlGames.Engine.ECS;

public abstract class Entity
{
    private static HashSet<Entity> entities = new();
    public static Entity? GetById(Guid id) 
        => entities.SingleOrDefault(e => e.Id == id);

    public Guid Id { get; }
    
    private readonly HashSet<Component> components = new();

    protected Entity()
    {
        this.Id = Guid.NewGuid();
        entities.Add(this);
    }

    public void AddComponent<TComponent>()
        where TComponent : Component, new()
    {
        this.AddComponent(new TComponent());
    }
    
    public void AddComponent(Component component)
    {
        component.Initialize(this);
        this.components.Add(component);
    }

    public TComponent? GetComponent<TComponent>()
        where TComponent : Component
    {
        return this.components.Single(c 
            => c.GetType() == typeof(TComponent)) as TComponent;
    }
}