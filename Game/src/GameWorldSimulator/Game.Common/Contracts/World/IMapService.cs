using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Items.Types;
using Game.Common.Contracts.World.Tiles;

namespace Game.Common.Contracts.World;

public interface IMapService
{
    void ReplaceGround(Location.Structs.Location location, IGround ground);
    ITile GetFinalTile(Location.Structs.Location location);

    bool GetNeighbourAvailableTile(Location.Structs.Location location, ICreature creature, ITileEnterRule rule,
        out ITile foundTile);
}