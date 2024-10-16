﻿using System;
using Game.Combat;
using Game.Combat.Validation;
using Server.Entities.Common.Contracts.World;
using Server.Entities.Common.Helpers;
using Server.Entities.Common.Location;

namespace Game.Creatures.Monster.Combat;

internal static class TargetDetector
{
    /// <summary>
    ///     Updates monster target list
    /// </summary>
    public static void UpdateTargets(Monster monster, IMapTool mapTool)
    {
        if (monster.Targets.IsNull()) return;

        var nearest = ushort.MaxValue;
        var nearestSightClear = ushort.MaxValue;

        monster.Targets.NearestTarget = null;
        monster.Targets.NearestSightClearTarget = null;

        foreach (CombatTarget target in monster.Targets)
        {
            target.ResetFlags();

            if (target.Creature.IsDead)
            {
                monster.Targets.RemoveTarget(target.Creature);
                continue;
            }

            if (mapTool.SightClearChecker.Invoke(monster.Location, target.Creature.Location, true) &&
                target.IsInRange(monster))
            {
                target.SetAsHasSightClear();

                var offsetSightClear = monster.Location.GetSqmDistance(target.Creature.Location);

                if (offsetSightClear >= nearestSightClear) continue;
                nearestSightClear = offsetSightClear;
                monster.Targets.NearestSightClearTarget = target;
            }

            var targetIsUnreachable = IsTargetUnreachable(monster, target, mapTool);
            if (targetIsUnreachable.Founded) continue;

            target.SetAsReachable(targetIsUnreachable.Directions);

            var offset = monster.Location.GetSqmDistance(target.Creature.Location);

            if (offset >= nearest) continue;

            nearest = offset;
            monster.Targets.NearestTarget = target;
        }
    }

    private static (bool Founded, Direction[] Directions) IsTargetUnreachable(Monster monster, CombatTarget target,
        IMapTool mapTool)
    {
        var result = mapTool.PathFinder.Find(monster, target.Creature.Location, monster.PathSearchParams,
            monster.TileEnterRule);

        if (!result.Founded) return (true, Array.Empty<Direction>());

        if (AttackValidation.CanAttack(monster, target.Creature).Failed) return result;

        if (target.Creature.IsInvisible && !monster.CanSeeInvisible) return result;

        return (false, Array.Empty<Direction>());
    }
}