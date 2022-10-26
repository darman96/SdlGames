using System.Collections.Immutable;
using System.Reflection;

namespace SdlGames.Engine.ECS;

internal class ComponentStore
{
    private readonly Dictionary<Type, HashSet<(Guid, object)>> store = new();
    
    public void RegisterComponent(Guid entityId, object component)
    {
        var componentType = component.GetType();
        if (!this.store.ContainsKey(componentType))
            this.store[componentType] = new HashSet<(Guid, object)>();
        
        this.store[componentType].Add((entityId, component));
    }
    
    public ImmutableHashSet<(Guid, TComponent)> GetComponents<TComponent>()
    {
        var componentType = typeof(TComponent);
        if (!this.store.ContainsKey(componentType))
            return ImmutableHashSet<(Guid, TComponent)>.Empty;
        
        return this.store[componentType]
            .Cast<(Guid, TComponent)>()
            .ToImmutableHashSet();
    }

    public ImmutableHashSet<(Guid, TComponentCollection)> GetComponentCollections<TComponentCollection>()
        where TComponentCollection : struct
    {
        var componentTypes = typeof(TComponentCollection)
            .GetFields(BindingFlags.Instance | BindingFlags.Public)
            .Select(info => this.store.ContainsKey(info.FieldType) 
                ? info.FieldType
                : throw new KeyNotFoundException($"Component type not found: {info.FieldType}"));
        
        var componentCollections = new HashSet<(Guid, TComponentCollection)>();
        
        foreach (var componentType in componentTypes)
        {
            foreach (var (entityId, component) in this.store[componentType])
            {
                var componentCollection = componentCollections.FirstOrDefault(x => x.Item1 == entityId);
                if (componentCollection.Item1 == Guid.Empty)
                {
                    componentCollection.Item1 = entityId;
                    componentCollection.Item2 = new TComponentCollection();
                    componentCollections.Add(componentCollection);
                }
                
                typeof(TComponentCollection)
                    .GetField(component.GetType().Name)!
                    .SetValue(componentCollection.Item2, component);
            }
        }

        return componentCollections.ToImmutableHashSet();
    }

    public void RemoveComponent<TComponent>(Guid entityId)
        => this.RemoveComponent(entityId, typeof(TComponent));
    
    public void RemoveComponent(Guid entityId, Type componentType)
    {
        if (!this.store.ContainsKey(componentType))
            throw new KeyNotFoundException($"Component type not found: {componentType}");
        
        this.store[componentType].RemoveWhere(x => x.Item1 == entityId);
    }
    
}