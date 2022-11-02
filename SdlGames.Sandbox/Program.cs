using System.Drawing;
using System.Numerics;
using SdlGames.Engine;
using SdlGames.Engine.ECS.Component;
using SdlGames.Engine.ECS.Entity;
using SdlGames.Engine.Graphics;
using SdlGames.Sandbox.Components;
using SdlGames.Sandbox.Systems;

namespace SdlGames.Sandbox;

public class TestGame : Game
{
    private SpriteSheet sprites;
    private Entity player;
    private Entity world;
    
    public TestGame(string title, int width, int height)
        : base(title, width, height) { }

    public override void Initialize()
    {
        var gameManager = GameManager.Instance;
        
        this.sprites = new SpriteSheet(
            gameManager.ResourceManager.LoadTexture("Assets/BoulderDashSprites.png"),
            new Size(32, 32));
        
        gameManager.AddSystem(new MovementSystem());
        gameManager.AddSystem(new TileSystem(this.sprites));

        this.world = gameManager.CreateEntity(
            new TileWorldComponent(12, 12, this.sprites));
        
        this.player = gameManager.CreateEntity();
        this.player.AddComponent(new TransformComponent(new PointF(32,32), Vector2.One));

        var playerAnimation = new SpriteAnimationComponent(
            this.sprites.GetSprites(27, 8), 8, true);
        this.player.AddComponent(playerAnimation);
        
        gameManager.PrintEntitiesWithComponents();
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