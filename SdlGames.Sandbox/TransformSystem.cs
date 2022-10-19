using SdlGames.Engine.ECS;

namespace SdlGames.Sandbox;

public struct TransformSystemComponents
{
    public TransformComponent Transform;
}

public class TransformSystem : System<TransformSystemComponents>
{
    protected override void OnUpdate(IEnumerable<TransformSystemComponents> components)
    {
    }
}