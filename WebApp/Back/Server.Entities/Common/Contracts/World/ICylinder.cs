using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Results;

namespace Server.Entities.Common.Contracts.World;

public interface ICylinder
{
    ICylinderSpectator[] TileSpectators { get; }
    ITile FromTile { get; }
    ITile ToTile { get; }
    IThing Thing { get; }
    Operation Operation { get; }

    bool IsTeleport { get; }
}

public interface ICylinderSpectator
{
    ICreature Spectator { get; }
    byte FromStackPosition { get; set; }
    byte ToStackPosition { get; set; }
}