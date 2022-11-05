using System.Drawing;
using SdlGames.Engine;
using SdlGames.Engine.ECS.Component;
using SdlGames.Engine.ECS.Entity;
using SdlGames.Engine.Event;
using SdlGames.Engine.Graphics;

namespace SdlGames.Sandbox;

public class TestGame : Game
{
    private SpriteSheet sprites;
    private Entity player;
    private TileWorld world;

    public TestGame(string title, int width, int height)
        : base(title, width, height) { }

    public override void Initialize()
    {
        this.sprites = new SpriteSheet(
            this.ResourceManager.LoadTexture("Assets/BoulderDashSprites.png"),
            new Size(32, 32));

        this.world = TileWorld.FromFile(
            "Assets/Map_01.map",
            new BoulderDashTileSet(this.sprites), 
            this);
        
        this.player = this.CreateEntity(
            new TransformComponent(new PointF(0, 0)),
            new SpriteComponent(this.sprites.GetSprite(0)));
    }

    public override void Update()
    {
    }

    public override void HandleEvent(EventType eventType)
    {
        Console.WriteLine($"Event: {eventType}");
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