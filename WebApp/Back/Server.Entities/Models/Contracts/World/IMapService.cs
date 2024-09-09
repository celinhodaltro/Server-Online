using Game.Common.Location.Structs;
using Server.Entities.Models.Contracts.Creatures;
using Server.Entities.Models.Contracts.Items.Types;
using Server.Entities.Models.Contracts.World.Tiles;

namespace Server.Entities.Models.Contracts.World;

public interface IMapService
{
    void ReplaceGround(Location location, IGround ground);
    ITile GetFinalTile(Location location);

    bool GetNeighbourAvailableTile(Location location, ICreature creature, ITileEnterRule rule,
        out ITile foundTile);
}