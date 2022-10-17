using System.Collections.Immutable;

namespace SdlGames.Engine.ECS;

public abstract class Component
{
    private Guid ownerId;

    public static ImmutableHashSet<TComponent> GetComponents<TComponent>()
        where TComponent : Component
        => ComponentPool.GetComponents<TComponent>();

    internal void Initialize(Entity owner)
    {
        this.ownerId = owner.Id;
        ComponentPool.RegisterComponent(owner.Id, this);
    }
    
    public Entity GetOwner()
    {
        var owner = Entity.GetById(this.ownerId);
        
        if (owner is null) 
            throw new Exception("Component with missing owner!");

        return owner;
    }
}

internal static class ComponentPool
{
    private static readonly Dictionary<Type, Dictionary<Guid, Component>> components = new();

    public static void RegisterComponent(Guid ownerId, Component component)
    {
        var componentType = component.GetType();
        if (!components.ContainsKey(componentType))
            components[componentType] = new Dictionary<Guid, Component>();

        components[componentType][ownerId] = component;
    }

    public static ImmutableHashSet<TComponent> GetComponents<TComponent>()
        where TComponent : Component
        => GetComponents(typeof(TComponent))
            .OfType<TComponent>()
            .ToImmutableHashSet();

    public static IEnumerable<Component> GetComponents(Type componentType)
    {
        if (!components.ContainsKey(componentType))
            throw new KeyNotFoundException($"No components of type {componentType} found");

        return components[componentType]
            .Values
            .ToImmutableHashSet();
    }
}

public class TransformComponent : Component
{
}

public class SpriteComponent : Component
{
}

