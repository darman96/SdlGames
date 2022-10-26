namespace SdlGames.Engine.ECS;

public class EntityStore
{
    private readonly HashSet<Entity> entities = new();

    internal EntityStore() { }
    
    public Entity CreateEntity()
    {
        var entity = new Entity(Guid.NewGuid());
        this.entities.Add(entity);
        return entity;
    }
}