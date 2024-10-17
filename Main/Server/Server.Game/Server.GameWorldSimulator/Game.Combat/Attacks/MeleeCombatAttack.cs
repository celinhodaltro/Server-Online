﻿using System;
using Game.Combat.Conditions;
using Server.Entities.Common.Combat.Structs;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Helpers;

namespace Game.Combat.Attacks;

public class MeleeCombatAttack : CombatAttack
{
    public MeleeCombatAttack(ushort min, ushort max, ConditionType conditionType, ushort conditionInterval)
    {
        Min = min;
        Max = max;
        ConditionType = conditionType;
        ConditionInterval = conditionInterval;
    }

    public MeleeCombatAttack()
    {
    }

    public ushort Min { get; set; }
    public ushort Max { get; set; }
    public ConditionType ConditionType { get; set; }
    public ushort ConditionInterval { get; set; }

    public static bool CalculateAttack(ICombatActor actor, ICombatActor enemy, CombatAttackValue combat,
        out CombatDamage damage)
    {
        damage = new CombatDamage();
        if (!actor.Location.IsNextTo(enemy.Location)) return false;

        var damageValue = (ushort)GameRandom.Random.NextInRange(combat.MinDamage, combat.MaxDamage);

        damage = new CombatDamage(damageValue, combat.DamageType);

        return true;
    }

    public static ushort CalculateMaxDamage(int skill, int attack)
    {
        return (ushort)Math.Ceiling(skill * (attack * 0.05) + attack * 0.5);
    }

    public override bool TryAttack(ICombatActor actor, ICombatActor enemy, CombatAttackValue option,
        out CombatAttackResult combatResult)
    {
        combatResult = new CombatAttackResult(option.DamageType);

        if (CalculateAttack(actor, enemy, option, out var damage))
        {
            var wasDamaged = enemy.ReceiveAttack(actor, damage);

            if (!wasDamaged) return true;

            if (ConditionType != ConditionType.None)
            {
                if (!enemy.HasCondition(ConditionType, out var condition))
                    enemy.AddCondition(new DamageCondition(ConditionType, ConditionInterval, Min, Max));
                else if (condition is DamageCondition damageCondition) damageCondition.Start(enemy, Min, Max);
                else condition.Start(enemy);
            }

            return true;
        }

        return false;
    }
}