using SdlGames.Engine.ECS;

namespace SdlGames.Sandbox;

public class PlayerEntity : Entity
{
    public PlayerEntity()
    {
        this.AddComponent<TransformComponent>();
    }
}