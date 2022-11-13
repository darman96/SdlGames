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
        var systemInfo = new SystemInfo(systemType);

        this.systems.Add(system);
        this.systemInfos[systemType] = systemInfo;
    }

    public void UpdateSystems()
    {
        this.systems.ForEach(this.UpdateSystem);
    }

    private void UpdateSystem(object system)
    {
        var systemType = system.GetType();
        var systemInfo = this.systemInfos[systemType];
        var componentTypes = systemInfo
            .ComponentInfos
            .Select(x => x.Type)
            .ToArray();

        try
        {
            var componentGroups = this.componentStore
                .GetComponentGroups(componentTypes);

            foreach (var componentGroup in componentGroups)
            {
                var componentInstances = componentGroup
                    .Components
                    .Join(
                        systemInfo.ComponentInfos,
                        component => component.GetType(),
                        info => info.Type,
                        (component, info) => (Info : info, Instance : component))
                    .OrderBy(x => x.Info.ParameterIndex)
                    .Select(x => x.Instance)
                    .ToArray();

                systemInfo.UpdateMethod.Invoke(
                    system,
                    componentInstances);
            }
        }
        catch (KeyNotFoundException) { }
    }
}