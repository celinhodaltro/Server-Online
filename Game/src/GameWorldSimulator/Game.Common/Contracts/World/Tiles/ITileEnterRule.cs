using Game.Common.Contracts.Creatures;

namespace Game.Common.Contracts.World.Tiles;

public interface ITileEnterRule
{
    bool ShouldIgnore(ITile tile, ICreature creature);
    bool CanEnter(ITile tile, ICreature creature);
}