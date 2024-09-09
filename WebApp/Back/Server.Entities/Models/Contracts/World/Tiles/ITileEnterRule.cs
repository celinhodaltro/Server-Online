using Server.Entities.Models.Contracts.Creatures;

namespace Server.Entities.Models.Contracts.World.Tiles;

public interface ITileEnterRule
{
    bool ShouldIgnore(ITile tile, ICreature creature);
    bool CanEnter(ITile tile, ICreature creature);
}