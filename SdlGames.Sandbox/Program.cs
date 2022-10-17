using SdlGames.Engine.ECS;

namespace SdlGames.Sandbox;

public static class Program
{
    public static void Main(string[] args)
    {
        var player = new PlayerEntity();
        var system = new TransformSystem();
        system.Update();
    }
}