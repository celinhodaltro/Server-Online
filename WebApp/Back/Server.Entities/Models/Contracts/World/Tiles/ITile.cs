using Game.Common.Location;
using Server.Entities.Models.Contracts.Creatures;
using Server.Entities.Models.Contracts.Items;

namespace Server.Entities.Models.Contracts.World.Tiles;

public interface ITile : IThing
{
    IItem TopItemOnStack { get; }
    ICreature TopCreatureOnStack { get; }
    bool BlockMissile { get; }
    int ThingsCount { get; }
    bool HasThings { get; }
    public bool ProtectionZone { get; }

    /// <summary>
    ///     check whether tile is 1 sqm distant to destination tile
    /// </summary>
    /// <returns></returns>
    public bool IsNextTo(ITile dest)
    {
        return Location.IsNextTo(dest.Location);
    }

    bool TryGetStackPositionOfThing(IPlayer player, IThing thing, out byte stackPosition);
    byte GetCreatureStackPositionIndex(IPlayer observer);
    bool HasFlag(TileFlags flag);
}