﻿using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Helpers;
using Server.Entities.Common.Results;

namespace Game.Combat.Validation;

public static class AttackValidation
{
    public static Result CanAttack(ICombatActor aggressor, ICombatActor victim)
    {
        if (Guard.AnyNull(aggressor, victim) || aggressor.Equals(victim)) return Result.NotPossible;

        if (victim.IsDead || aggressor.IsDead) return Result.Fail(InvalidOperation.CreatureIsDead);

        if (!aggressor.CanSee(victim.Location)) return Result.Fail(InvalidOperation.CreatureIsNotReachable);

        if (!aggressor.Location.SameFloorAs(victim.Location))
            return Result.Fail(InvalidOperation.CreatureIsNotReachable);

        if (aggressor.Tile?.ProtectionZone ?? false)
            return Result.Fail(InvalidOperation.CannotAttackWhileInProtectionZone);

        if (victim.Tile?.ProtectionZone ?? false)
            return Result.Fail(InvalidOperation.CannotAttackPersonInProtectionZone);

        return Result.Success;
    }
}