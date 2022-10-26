using SdlGames.Engine.ECS.Components;
using SdlGames.Engine.Internal.Interfaces;

namespace SdlGames.Engine.ECS.Systems;

public struct SpriteSystemComponents
{
    public TransformComponent Transform;
    public SpriteComponent Sprite;
}

public class SpriteSystem
{
    private readonly IRenderer renderer;

    public SpriteSystem(IRenderer renderer)
    {
        this.renderer = renderer;
    }
    
    public void Update(TransformComponent transformComponent, SpriteComponent spriteComponent)
    {
        renderer.DrawTexture(transformComponent.Position, spriteComponent.Texture);
    }
}