using SdlGames.Sandbox.Systems;

namespace SdlGames.Sandbox.Components;

public class TileComponent
{
    public Tile Tile;
    
    public TileComponent(Tile tile)
    {
        this.Tile = tile;
    }
}