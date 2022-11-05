using SdlGames.Engine.Graphics;

namespace SdlGames.Sandbox;

public enum BoulderDashTile
{
    None = 0,
    Wall = 1,
    Dirt = 2,
    Diamond = 3,
    Boulder = 4,
}

public class BoulderDashTileSet : TileSet<BoulderDashTile>
{
    public BoulderDashTileSet(SpriteSheet spriteSheet)
    {
        this.Tiles = new Dictionary<BoulderDashTile, Tile>
        {
            { BoulderDashTile.None, new Tile(spriteSheet.GetSprite(4)) },
            { BoulderDashTile.Wall, new Tile(spriteSheet.GetSprite(55)) },
            { BoulderDashTile.Dirt, new Tile(spriteSheet.GetSprite(64)) },
            { BoulderDashTile.Diamond, new Tile(spriteSheet.GetSpriteAnimation(90, 8, 8)) },
            { BoulderDashTile.Boulder, new Tile(spriteSheet.GetSprite(63)) },
        };
    }
}