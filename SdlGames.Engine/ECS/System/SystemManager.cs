using SdlGames.Engine.ECS.Component;
using SdlGames.Engine.Extensions;

namespace SdlGames.Engine.ECS.System;

public class SystemManager
{
    private readonly Dictionary<Type, SystemInfo> systemInfos = new();
    private readonly HashSet<object> systems = new();
    private readonly ComponentStore componentStore;

    internal SystemManager(ComponentStore componentStore)
    {
        this.componentStore = componentStore;
    }

    public void AddSystem(object system)
    {
        var systemType = system.GetType();
        var updateMethod = systemType.GetMethod("Update");

        if (updateMethod is null)
            throw new Exception("System has now Update method");

        var requiredComponents = updateMethod
            .GetParameters()
            .Select((param, index) => new SystemInfo.RequiredComponentInfo
            {
                ParameterIndex = index,
                Type = param.ParameterType
            })
            .ToArray();

        var systemInfo = new SystemInfo
        {
            RequiredComponents = requiredComponents,
            UpdateMethod = updateMethod
        };

        this.systems.Add(system);
        this.systemInfos[systemType] = systemInfo;
    }

    public void UpdateSystems()
    {
        this.systems.ForEach(this.updateSystem);
    }

    private void updateSystem(object system)
    {
        var systemType = system.GetType();
        var systemInfo = this.systemInfos[systemType];
        var componentTypes = systemInfo.RequiredComponents
            .Select(x => x.Type)
            .ToArray();

        try
        {
            var componentGroups = this.componentStore
                .GetComponentGroups(componentTypes);

            foreach (var componentGroup in componentGroups)
            {
                var requiredComponents = componentGroup.Components
                    .Join(
                        systemInfo.RequiredComponents,
                        component => component.GetType(),
                        info => info.Type,
                        (component, info) 
                            => new SystemInfo.RequiredComponent(info, component))
                    .OrderBy(x => x.Info.ParameterIndex);

                systemInfo.UpdateMethod.Invoke(
                    system,
                    requiredComponents
                        .Select(r => r.Instance)
                        .ToArray());
            }
        }
        catch (KeyNotFoundException _) { }
    }
}