using Server.Entities.Common.Contracts.Creatures;

namespace Server.Entities.Common.Contracts.World.Tiles;

public interface ITileEnterRule
{
    bool ShouldIgnore(ITile tile, ICreature creature);
    bool CanEnter(ITile tile, ICreature creature);
}