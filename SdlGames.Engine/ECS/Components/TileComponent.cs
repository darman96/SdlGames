using SdlGames.Engine.Graphics;

namespace SdlGames.Engine.ECS.Components;

public class TileComponent
{
    public Tile Tile;
    
    public TileComponent(Tile tile)
    {
        this.Tile = tile;
    }
}