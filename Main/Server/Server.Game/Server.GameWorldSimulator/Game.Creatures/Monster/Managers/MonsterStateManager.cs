﻿using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Services;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Helpers;

namespace Game.Creatures.Monster.Managers;

public static class MonsterStateManager
{
    public static void Run(IMonster monster, ISummonService summonService)
    {
        if (monster.IsDead) return;

        monster.UpdateState();

        if (monster.IsCurrentTargetUnreachable) monster.StopAttack();

        if (monster.State == MonsterState.LookingForEnemy)
        {
            monster.LookForNewEnemy();
            monster.Summon(summonService);
        }

        if (monster.State == MonsterState.InCombat)
        {
            monster.MoveAroundEnemy();

            if (!monster.Attacking)
            {
                monster.SelectTargetToAttack();
                return;
            }

            monster.Summon(summonService);

            if (monster.Metadata.TargetChance.Interval == 0) return;

            if (monster.Attacking &&
                monster.Metadata.TargetChance.Chance < GameRandom.Random.Next(1, maxValue: 100)) return;
            monster.SelectTargetToAttack();
        }

        if (monster.State == MonsterState.Sleeping) monster.Sleep();
        if (monster.State == MonsterState.Escaping) monster.Escape();
    }
}