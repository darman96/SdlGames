using System.Numerics;

namespace SdlGames.Engine.ECS.Components;

public struct TransformComponent
{
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    
    
    public TransformComponent(Vector2 position, Vector2 scale)
    {
        this.Position = position;
        this.Scale = scale;
    }
}