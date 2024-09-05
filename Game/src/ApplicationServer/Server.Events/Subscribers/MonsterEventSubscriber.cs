using Game.Common.Contracts.Creatures;
using Server.Events.Combat;
using Server.Events.Creature;
using Server.Events.Creature.Monsters;

namespace Server.Events.Subscribers;

public class MonsterEventSubscriber : ICreatureEventSubscriber
{
    private readonly CreatureAttackEventHandler _creatureAttackEventHandler;

    private readonly CreatureWasBornEventHandler _creatureWasBornEventHandler;
    private readonly MonsterChangedStateEventHandler monsterChangedStateEventHandler;

    public MonsterEventSubscriber(CreatureWasBornEventHandler creatureWasBornEventHandler,
        CreatureAttackEventHandler creatureAttackEventHandler,
        MonsterChangedStateEventHandler monsterChangedStateEventHandler)
    {
        _creatureWasBornEventHandler = creatureWasBornEventHandler;
        _creatureAttackEventHandler = creatureAttackEventHandler;
        this.monsterChangedStateEventHandler = monsterChangedStateEventHandler;
    }

    public void Subscribe(ICreature creature)
    {
        if (creature is not IMonster monster) return;

        monster.OnWasBorn += _creatureWasBornEventHandler.Execute;
        monster.OnAttackEnemy += _creatureAttackEventHandler.Execute;
        monster.OnChangedState += monsterChangedStateEventHandler.Execute;
    }

    public void Unsubscribe(ICreature creature)
    {
        if (creature is not IMonster monster) return;

        monster.OnWasBorn -= _creatureWasBornEventHandler.Execute;
        monster.OnAttackEnemy -= _creatureAttackEventHandler.Execute;
        monster.OnChangedState -= monsterChangedStateEventHandler.Execute;
    }
}