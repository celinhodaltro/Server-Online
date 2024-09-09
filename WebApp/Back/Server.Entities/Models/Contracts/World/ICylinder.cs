using Server.Entities.Models.Contracts.Creatures;
using Server.Entities.Models.Contracts.Items;
using Server.Entities.Models.Contracts.World.Tiles;
using Server.Entities.Models.Results;

namespace Server.Entities.Models.Contracts.World;

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