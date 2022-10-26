using System.Numerics;
using SdlGames.Engine;
using SdlGames.Engine.ECS;
using SdlGames.Engine.ECS.Components;

namespace SdlGames.Sandbox;

public class TestGame : Game
{
    private Entity player;
    
    public TestGame(string title, int width, int height)
        : base(title, width, height) { }

    public override void Initialize()
    {
        var gameManager = GameManager.Instance;
        this.player = gameManager.CreateEntity();
        this.player.AddComponent(new TransformComponent(new Vector2(32,32), Vector2.One));
        
        var spriteComponent = new SpriteComponent(
            gameManager.ResourceManager.LoadTexture("Assets/BoulderDashSprites.png"),
            new Vector2(32, 32), 16);
        this.player.AddComponent(spriteComponent);
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