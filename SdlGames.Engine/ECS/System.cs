using System.Reflection;

namespace SdlGames.Engine.ECS;

public abstract class System<TComponents>
    where TComponents : struct
{
    private readonly HashSet<Type> componentTypes;

    protected System()
    {
        this.componentTypes = typeof(TComponents)
            .GetFields(BindingFlags.Public | BindingFlags.Instance)
            .Select(field => field.FieldType)
            .ToHashSet();
    }

    private IEnumerable<TComponents> GetComponents()
    {
        var componentsByEntity = this.componentTypes.Select(componentType =>
        {
            return ComponentPool
                .GetComponents(componentType)
                .Select(component =>
                {
                    var entity = component.GetOwner();
                    return (entity, component, componentType);
                });
        });

        return componentsByEntity
            .SelectMany(x 
                => x.GroupBy(y => y.entity))
            .Select(g =>
            {
                var components = new TComponents();
                foreach (var componentWithType in g)
                {
                    components
                        .GetType()
                        .GetFields()
                        .First(f => f.FieldType == componentWithType.componentType)
                        .SetValue(components, componentWithType.component);
                }

                return components;
            });
    }

    public void Update()
        => this.OnUpdate(this.GetComponents());

    protected abstract void OnUpdate(IEnumerable<TComponents> components);
}

public struct TransformSystemComponents
{
    public TransformComponent Transform;
}

public class TransformSystem : System<TransformSystemComponents>
{
    protected override void OnUpdate(IEnumerable<TransformSystemComponents> components)
    {
        Console.WriteLine(components.Count());
    }
}