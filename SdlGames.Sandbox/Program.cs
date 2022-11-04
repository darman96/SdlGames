using SdlGames.Engine;
using SdlGames.Engine.ECS.Entity;
using SdlGames.Engine.Event;
using SdlGames.Engine.Graphics;

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