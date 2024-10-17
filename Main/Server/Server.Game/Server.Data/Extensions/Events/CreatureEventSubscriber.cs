using Extensions.Events.Creatures;
using Extensions.Npcs;
using Server.Entities.Common.Contracts;
using Server.Entities.Common.Contracts.Creatures;

namespace Extensions.Events;

public class CreatureEventSubscriber : ICreatureEventSubscriber, IGameEventSubscriber
{
    private readonly CreatureDroppedLootEventHandler creatureDroppedLootEventHandler;
    private readonly CreatureKilledEventHandler creatureKilledEventHandler;

    public CreatureEventSubscriber(CreatureDroppedLootEventHandler creatureDroppedLootEventHandler,
        CreatureKilledEventHandler creatureKilledEventHandler)
    {
        this.creatureDroppedLootEventHandler = creatureDroppedLootEventHandler;
        this.creatureKilledEventHandler = creatureKilledEventHandler;
    }

    public void Subscribe(ICreature creature)
    {
        if (creature is ICombatActor actor)
        {
            actor.OnKilled += creatureKilledEventHandler.Execute;
            actor.OnKilled += creatureDroppedLootEventHandler.Execute;
        }

        if (creature is INpc npc) npc.OnAnswer += NpcActionHandler.OnAnswer;
    }

    public void Unsubscribe(ICreature creature)
    {
        if (creature is ICombatActor actor)
        {
            actor.OnKilled -= creatureKilledEventHandler.Execute;
            actor.OnKilled -= creatureDroppedLootEventHandler.Execute;
        }

        if (creature is INpc npc) npc.OnAnswer -= NpcActionHandler.OnAnswer;
    }
}