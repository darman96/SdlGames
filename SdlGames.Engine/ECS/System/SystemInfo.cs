using System.Reflection;

namespace SdlGames.Engine.ECS.System;

internal struct SystemInfo
{
    public struct RequiredComponentInfo
    {
        public int ParameterIndex;
        public Type Type;
    }

    public struct RequiredComponent
    {
        public RequiredComponentInfo Info;
        public object Instance;

        public RequiredComponent(RequiredComponentInfo info, object instance)
        {
            Info = info;
            Instance = instance;
        }
    }

    public RequiredComponentInfo[] RequiredComponents;
    public MethodInfo UpdateMethod;
}