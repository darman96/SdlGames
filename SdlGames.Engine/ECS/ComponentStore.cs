using System.Collections.Immutable;
using System.Reflection;
using SdlGames.Engine.Extensions;

namespace SdlGames.Engine.ECS;

internal class ComponentStore
{
    public struct ComponentInstance
    {
        public Guid EntityId;
        public object Component;

        public ComponentInstance(Guid entityId, object component)
        {
            EntityId = entityId;
            Component = component;
        }
    }

    public struct ComponentGroup
    {
        public Guid EntityId;
        public HashSet<object> Components;

        public ComponentGroup(Guid entityId, HashSet<object> components)
        {
            EntityId = entityId;
            Components = components;
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
            if (!store.ContainsKey(t))
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