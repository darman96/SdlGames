using System.Numerics;
using SdlGames.Engine;
using SdlGames.Engine.ECS.Component;
using SdlGames.Engine.ECS.Entity;

namespace SdlGames.Sandbox;

public class TestGame : Game
{
    private Entity player;
    
    public TestGame(string title, int width, int height)
        : base(title, width, height) { }

    public override void Initialize()
    {
        var gameManager = GameManager.Instance;
        gameManager.AddSystem(new MovementSystem());
        
        this.player = gameManager.CreateEntity();
        this.player.AddComponent(new TransformComponent(new Vector2(32,32), Vector2.One));
        
        var spriteComponent = new SpriteComponent(
            gameManager.ResourceManager.LoadTexture("Assets/BoulderDashSprites.png"),
            new Vector2(32, 32), 16);
        this.player.AddComponent(spriteComponent);
        this.player.AddComponent(new MovementComponent(1f));
    }
}

public class MovementComponent
{
    public float Speed;
    
    public MovementComponent(float speed = 1f)
    {
        this.Speed = speed;
    }
}

public class MovementSystem
{
    public void Update(TransformComponent transformComponent, MovementComponent movementComponent)
    {
        transformComponent.Position = transformComponent.Position 
            with { X = transformComponent.Position.X + movementComponent.Speed * GameManager.DeltaTime };
    }
}

public static class Program
{
    public static void Main(string[] args)
    {
        var game = new TestGame("Test", 800, 600);
        game.Run();
    }
}