﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures;

namespace Server.Events.Creature.Monsters;

public class MonsterChangedStateEventHandler
{
    private readonly ICreatureGameInstance creatureGameInstance;

    public MonsterChangedStateEventHandler(ICreatureGameInstance creatureGameInstance)
    {
        this.creatureGameInstance = creatureGameInstance;
    }

    public void Execute(IMonster monster, MonsterState oldState, MonsterState toState)
    {
        if (toState == MonsterState.Sleeping)
            creatureGameInstance.TryRemove(monster.CreatureId);
        else if (oldState == MonsterState.Sleeping) creatureGameInstance.Add(monster);
    }
}