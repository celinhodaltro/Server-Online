using Server.Entities.Models.Location;
using Server.Entities.Models.Location.Structs;
using Server.Entities.Models.Contracts.Creatures;
using Server.Entities.Models.Contracts.World.Tiles;

namespace Server.Entities.Models.Contracts.World;
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