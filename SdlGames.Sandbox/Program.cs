using SdlGames.Engine;
using SdlGames.Engine.ECS;

namespace SdlGames.Sandbox;

public class TestGame : Game
{
    public TestGame(string title, int width, int height) 
        : base(title, width, height) { }

    public override void OnUpdate()
    {
        this.window.Clear(Color.Red());
        this.window.Present();
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