using Server.Entities.Models.Contracts.Creatures;
using Server.Entities.Models.Contracts.Items.Types;
using Server.Entities.Models.Contracts.World.Tiles;

namespace Server.Entities.Models.Contracts.World;

public interface IMapService
{
    void ReplaceGround(Location.Structs.Location location, IGround ground);
    ITile GetFinalTile(Location.Structs.Location location);

    bool GetNeighbourAvailableTile(Location.Structs.Location location, ICreature creature, ITileEnterRule rule,
        out ITile foundTile);
}