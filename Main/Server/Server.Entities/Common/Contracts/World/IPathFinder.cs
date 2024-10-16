using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.World.Tiles;
using Server.Entities.Common.Location;
using Server.Entities.Common.Location.Structs;

namespace Server.Entities.Common.Contracts.World;

public interface IPathFinder
{
    IMap Map { get; set; }

    (bool Founded, Direction[] Directions) Find(ICreature creature, Location.Structs.Location target,
        FindPathParams findPathParams,
        ITileEnterRule tileEnterRule);

    (bool Founded, Direction[] Directions) Find(ICreature creature, Location.Structs.Location target,
        ITileEnterRule tileEnterRule);

    Direction FindRandomStep(ICreature creature, ITileEnterRule rule);

    Direction FindRandomStep(ICreature creature, ITileEnterRule rule, Location.Structs.Location origin,
        int maxStepsFromOrigin = 1);
}