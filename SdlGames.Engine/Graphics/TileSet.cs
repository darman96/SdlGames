namespace SdlGames.Engine.Graphics;

public abstract class TileSet<TEnum>
    where TEnum : Enum
{
    protected Dictionary<TEnum, Tile> Tiles { get; init; } = default!;
    
    public Tile this[TEnum index] => this.Tiles[index];
}