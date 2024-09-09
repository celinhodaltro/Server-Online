using Game.Common.Location;
using Game.Common.Location.Structs;
using Server.Entities.Models.Contracts.Creatures;
using Server.Entities.Models.Contracts.World.Tiles;

namespace Server.Entities.Models.Contracts.World;
public interface IPathFinder
{
    IMap Map { get; set; }

    (bool Founded, Direction[] Directions) Find(ICreature creature, Location target,
        FindPathParams findPathParams,
        ITileEnterRule tileEnterRule);

    (bool Founded, Direction[] Directions) Find(ICreature creature, Location target,
        ITileEnterRule tileEnterRule);

    Direction FindRandomStep(ICreature creature, ITileEnterRule rule);

    Direction FindRandomStep(ICreature creature, ITileEnterRule rule, Location origin,
        int maxStepsFromOrigin = 1);
}