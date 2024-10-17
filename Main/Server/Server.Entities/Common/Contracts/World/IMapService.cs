using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Items.Types;
using Server.Entities.Common.Contracts.World.Tiles;

namespace Server.Entities.Common.Contracts.World;

public interface IMapService
{
    void ReplaceGround(Location.Structs.Location location, IGround ground);
    ITile GetFinalTile(Location.Structs.Location location);

    bool GetNeighbourAvailableTile(Location.Structs.Location location, ICreature creature, ITileEnterRule rule,
        out ITile foundTile);
}