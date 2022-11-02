using System.Drawing;
using System.Numerics;
using SdlGames.Engine;
using SdlGames.Engine.ECS.Component;
using SdlGames.Engine.ECS.Entity;
using SdlGames.Engine.Graphics;
using SdlGames.Sandbox.Systems;

namespace SdlGames.Sandbox.Components;

public class TileWorldComponent
{
    public Entity[] Tiles { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    
    public TileWorldComponent(uint width, uint height, SpriteSheet sprites)
    {
        this.Tiles = new Entity[width * height];
        this.Width = (int)width;
        this.Height = (int)height;
        
        var tileCount = width * height;
        for (int i = 0; i < tileCount; i++)
        {
            var tilePosition = new PointF(32 * (i % width), 32 * (i / width));
            
            this.Tiles[i] = GameManager.Instance.CreateEntity(
                new TileComponent(Tile.Dirt),
                new TransformComponent(tilePosition, Vector2.One),
                new SpriteComponent(sprites.GetSprite((int)Tile.Dirt)));
        }
    }
}