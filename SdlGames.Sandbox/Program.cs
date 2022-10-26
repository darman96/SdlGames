using SdlGames.Engine;

namespace SdlGames.Sandbox;

public class TestGame : Game
{
    public TestGame(string title, int width, int height)
        : base(title, width, height) { }

    public override void Initialize()
    {
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