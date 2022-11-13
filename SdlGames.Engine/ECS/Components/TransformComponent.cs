using System.Drawing;
using System.Numerics;

namespace SdlGames.Engine.ECS.Components;

public class TransformComponent
{
    public PointF Position { get; set; }
    public Vector2 Scale { get; set; }
    
    public TransformComponent(PointF position)
        : this(position, Vector2.One) {}
    
    public TransformComponent(PointF position, Vector2 scale)
    {
        this.Position = position;
        this.Scale = scale;
    }
}