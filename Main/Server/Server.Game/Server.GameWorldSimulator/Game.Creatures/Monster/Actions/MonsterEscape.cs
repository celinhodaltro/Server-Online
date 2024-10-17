﻿using Game.Combat;
using Server.Entities.Common.Contracts.Creatures;
using Game.Creatures.Monster.Combat;

namespace Game.Creatures.Monster.Actions;

internal static class MonsterEscape
{
    public static void Escape(Monster monster)
    {
        monster.StopFollowing();
        monster.StopAttack();

        var targets = monster.Targets;

        var escapeFrom = FindTarget(monster, targets);

        if (escapeFrom is null) return;

        monster.Escape(escapeFrom.Location);
    }

    private static ICreature FindTarget(ICombatActor monster, TargetList targets)
    {
        if (targets.TryGetTarget(monster.AutoAttackTargetId, out var creature)) return creature.Creature;

        ICreature escapeFrom = null;

        foreach (CombatTarget target in targets)
        {
            if (target.CanReachCreature)
            {
                escapeFrom = target.Creature;
                break;
            }

            escapeFrom = target.Creature;
        }

        return escapeFrom;
    }
}