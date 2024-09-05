using Game.Common.Contracts;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.World;
using Game.Common.Contracts.World.Tiles;
using Game.Common.Location;
using Game.Common.Location.Structs;

namespace Game.Creatures.Events;

public class CreatureTeleportedEventHandler : IGameEventHandler
{
    private readonly IMap map;

    public CreatureTeleportedEventHandler(IMap map)
    {
        this.map = map;
    }

    public void Execute(IWalkableCreature creature, Location location)
    {
        if (creature.Location == location) return;

        if (map[location] is not IDynamicTile { FloorDirection: FloorChangeDirection.None } tile)
        {
            foreach (var neighbour in location.Neighbours)
                if (map[neighbour] is IDynamicTile toTile)
                {
                    map.TryMoveCreature(creature, toTile.Location);
                    return;
                }
        }
        else
        {
            map.TryMoveCreature(creature, tile.Location);
        }
    }
}