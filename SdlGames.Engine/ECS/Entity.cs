namespace SdlGames.Engine.ECS;

public struct Entity
{
    public Guid Id { get; init; }

    internal Entity(Guid id) { this.Id = id; }
}