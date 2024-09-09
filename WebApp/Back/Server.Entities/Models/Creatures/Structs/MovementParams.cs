using Game.Common.Location.Structs;

namespace Server.Entities.Models.Creatures.Structs;

public readonly ref struct MovementParams
{
    public MovementParams(Location fromLocation, Location toLocation, byte amount)
    {
        FromLocation = fromLocation;
        ToLocation = toLocation;
        Amount = amount;
    }

    public Location FromLocation { get; }
    public Location ToLocation { get; }
    public byte Amount { get; }
}