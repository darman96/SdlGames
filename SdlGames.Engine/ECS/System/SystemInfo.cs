using System.Reflection;

namespace SdlGames.Engine.ECS.System;

internal class SystemInfo
{
    public readonly SystemComponentInfo[] ComponentInfos;
    public readonly MethodInfo UpdateMethod;

    public SystemInfo(Type systemType)
    {
        var updateMethod = systemType
            .GetMethod("Update");

        this.UpdateMethod = updateMethod 
            ?? throw new ArgumentException($"System {systemType.Name} does not have an Update method");
        this.ComponentInfos = updateMethod.GetParameters()
            .Select((param, idx) 
                => new SystemComponentInfo(param, idx))
            .ToArray();
    }
}