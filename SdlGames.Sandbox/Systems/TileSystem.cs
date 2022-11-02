using SdlGames.Engine.ECS.Component;
using SdlGames.Engine.Graphics;
using SdlGames.Sandbox.Components;

namespace SdlGames.Sandbox.Systems;

public enum Tile
{
    Empty = 54,
    Dirt = 64,
    BedRock = 55,
    Boulder = 63
}

public class TileSystem
{
    private readonly SpriteSheet sprites;
    private readonly Dictionary<Tile, Sprite> spriteMap = new();

    public TileSystem(SpriteSheet sprites)
    {
        this.sprites = sprites;
        this.spriteMap.Add(Tile.Empty, sprites.GetSprite(54));
        this.spriteMap.Add(Tile.Dirt, sprites.GetSprite(64));
        this.spriteMap.Add(Tile.BedRock, sprites.GetSprite(55));
        this.spriteMap.Add(Tile.Boulder, sprites.GetSprite(63));
    }

    public void Update(TileComponent tileComponent, SpriteComponent spriteComponent)
    {
        spriteComponent.Sprite = this.spriteMap[tileComponent.Tile];
    }
}