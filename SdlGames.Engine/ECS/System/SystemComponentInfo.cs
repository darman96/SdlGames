using System.Reflection;

namespace SdlGames.Engine.ECS.System;

public class SystemComponentInfo
{
    public int ParameterIndex { get; }
    public Type Type { get; }
    public bool Optional { get; }
    
    public SystemComponentInfo(ParameterInfo parameterInfo, int idx)
    {
        var optional = Nullable
            .GetUnderlyingType(parameterInfo.ParameterType) != null;
        
        this.ParameterIndex = idx;
        this.Type = optional 
            ? parameterInfo.ParameterType.GetGenericArguments()[0]
            : parameterInfo.ParameterType;
        this.Optional = optional;
    }
}