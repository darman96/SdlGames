using System.Collections.Immutable;
using SdlGames.Engine.Extensions;

namespace SdlGames.Engine.ECS.Component;

internal class ComponentStore
{
    public struct ComponentInstance
    {
        public Guid EntityId;
        public object Component;

        public ComponentInstance(Guid entityId, object component)
        {
            this.EntityId = entityId;
            this.Component = component;
        }
    }

    public struct ComponentGroup
    {
        public Guid EntityId;
        public HashSet<object> Components;

        public ComponentGroup(Guid entityId, HashSet<object> components)
        {
            this.EntityId = entityId;
            this.Components = components;
        }
    }

    private readonly Dictionary<Type, HashSet<ComponentInstance>> store = new();

    public void RegisterComponent(Guid entityId, object component)
    {
        var componentType = component.GetType();
        if (!this.store.ContainsKey(componentType))
            this.store[componentType] = new HashSet<ComponentInstance>();

        this.store[componentType].Add(new ComponentInstance(entityId, component));
    }

    public bool HasComponent<T>(Guid entityId)
    {
        var componentType = typeof(T);
        if (!this.store.ContainsKey(componentType))
            throw new KeyNotFoundException($"Component type not found: {componentType}");

        return this.store[componentType]
            .Any(x => x.EntityId == entityId);
    }

    public T GetComponent<T>(Guid entityId)
    {
        var componentType = typeof(T);
        if (!this.store.ContainsKey(componentType))
            throw new KeyNotFoundException($"Component type not found: {componentType}");
        
        if (!this.store[componentType].Any(x => x.EntityId == entityId))
            throw new KeyNotFoundException($"Component {componentType} not found for entity: {entityId}");

        return (T)this.store[componentType]
            .Single(x => x.EntityId == entityId)
            .Component;
    }
    
    public ImmutableHashSet<ComponentInstance> GetComponents(Guid entityId)
    {
        return this.store
            .SelectMany(x => x.Value)
            .Where(x => x.EntityId == entityId)
            .ToImmutableHashSet();
    }
    
    public ImmutableHashSet<ComponentInstance> GetComponents<TComponent>()
    {
        var componentType = typeof(TComponent);
        if (!this.store.ContainsKey(componentType))
            return ImmutableHashSet<ComponentInstance>.Empty;

        return this.store[componentType]
            .ToImmutableHashSet();
    }

    public ImmutableHashSet<ComponentGroup> GetComponentGroups(Type[] componentTypes)
    {
        componentTypes.ForEach(t =>
        {
            if (!this.store.ContainsKey(t))
                throw new KeyNotFoundException($"Component type not found: {t}");
        });

        var componentGroups = new HashSet<ComponentGroup>();

        foreach (var componentType in componentTypes)
        {
            foreach (var instance in this.store[componentType])
            {
                var componentGroup = componentGroups
                    .FirstOrDefault(x => x.EntityId == instance.EntityId);
                if (componentGroup.EntityId == Guid.Empty)
                {
                    componentGroup.EntityId = instance.EntityId;
                    componentGroup.Components = new HashSet<object>();
                    componentGroups.Add(componentGroup);
                }

                componentGroup.Components.Add(instance.Component);
            }
        }

        return componentGroups
            .Where(g => g.Components.Count == componentTypes.Length)
            .ToImmutableHashSet();
    }

    public void RemoveComponent<TComponent>(Guid entityId)
        => this.RemoveComponent(entityId, typeof(TComponent));

    public void RemoveComponent(Guid entityId, Type componentType)
    {
        if (!this.store.ContainsKey(componentType))
            throw new KeyNotFoundException($"Component type not found: {componentType}");

        this.store[componentType].RemoveWhere(x => x.EntityId == entityId);
    }
}