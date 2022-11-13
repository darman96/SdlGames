using System.Drawing;
using SdlGames.Engine;
using SdlGames.Engine.ECS.Components;
using SdlGames.Engine.ECS.Entity;
using SdlGames.Engine.Graphics;

namespace SdlGames.Sandbox;

public class TileWorld
{
    private int width;
    private int height;
    private Entity[] tiles;
    
    public static TileWorld FromFile<TEnum>(string filename, TileSet<TEnum> tileSet, Game game)
        where TEnum : struct, Enum
    {
        var lines = File.ReadAllLines(filename);
        var world = new TileWorld(lines[0].Length, lines.Length);
        for (var y = 0; y < lines.Length; y++)
        {
            for (var x = 0; x < lines[y].Length; x++)
            {
                var tileType = Enum.Parse<TEnum>(lines[y][x].ToString(), true);
                var tile = tileSet[tileType];
                world.tiles[x + y * world.width] = CreateTileEntity(tile, game, x, y, 32);
            }
        }
        return world;
    }

    private TileWorld(int width, int height)
    {
        this.width = width;
        this.height = height;
        this.tiles = new Entity[height * width];
    }

    private static Entity CreateTileEntity(Tile tile, Game game, int x, int y, int size)
    {
        var position = new PointF(x * size, y * size);
        if (tile.SpriteAnimation is {} spriteAnimation)
            return game.CreateEntity(
                new TileComponent(tile),
                new SpriteAnimationComponent(spriteAnimation, true),
                new TransformComponent(position));
        if (tile.Sprite is {} sprite)
            return game.CreateEntity(
                new TileComponent(tile),
                new SpriteComponent(sprite),
                new TransformComponent(position));
        
        throw new Exception("Tile has no sprite or sprite animation");
    }
}