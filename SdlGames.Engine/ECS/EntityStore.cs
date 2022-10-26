using SdlGames.Engine.Extensions;

namespace SdlGames.Engine.ECS;

public class EntityStore
{
    private readonly ComponentStore componentStore;
    private readonly HashSet<Entity> entities = new();

    internal EntityStore(ComponentStore componentStore)
    {
        this.componentStore = componentStore;
    }
    
    public Entity CreateEntity(params object[] components)
    {
        var entity = new Entity(Guid.NewGuid());
        this.entities.Add(entity);
        
        components.ForEach(c 
            => componentStore.RegisterComponent(entity.Id, c));
        
        return entity;
    }

    public Entity? GetEntity(Guid id)
    {
        return this.entities
            .FirstOrDefault(e => e.Id == id);
    }
}