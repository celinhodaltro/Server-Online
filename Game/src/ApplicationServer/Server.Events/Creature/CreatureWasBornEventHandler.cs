using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Location.Structs;

namespace Server.Events.Creature;

public class CreatureWasBornEventHandler
{
    private readonly IMap map;

    public CreatureWasBornEventHandler(IMap map)
    {
        this.map = map;
    }

    public void Execute(IMonster creature, Location location)
    {
        map.PlaceCreature(creature);
    }
}