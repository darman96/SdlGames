using SdlGames.Engine.ECS.Component;
using SdlGames.Engine.Extensions;

namespace SdlGames.Engine.ECS.Entity;

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
        var entity = new Entity(Guid.NewGuid(), this.componentStore);
        this.entities.Add(entity);
        
        components.ForEach(c 
            => this.componentStore.RegisterComponent(entity.Id, c));
        
        return entity;
    }

    public Entity? GetEntity(Guid id)
        => this.entities.FirstOrDefault(e => e.Id == id);
}