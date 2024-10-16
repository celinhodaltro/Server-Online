﻿using Server.Entities.Common.Combat.Structs;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Effects.Magical;

namespace Game.Combat.Attacks;

public class SpreadCombatAttack : CombatAttack
{
    public SpreadCombatAttack(byte length, byte spread)
    {
        Length = length;
        Spread = spread;
    }

    public byte Spread { get; }
    public byte Length { get; }

    public override bool TryAttack(ICombatActor actor, ICombatActor enemy, CombatAttackValue option,
        out CombatAttackResult combatResult)
    {
        combatResult = new CombatAttackResult(option.DamageType)
        {
            EffectT = option.DamageEffect
        };

        if (CalculateAttack(actor, enemy, option, out var damage))
        {
            combatResult.SetArea(SpreadEffect.Create(actor.Location, actor.Direction, Length, Spread));
            actor.PropagateAttack(combatResult.Area, damage);
            return true;
        }

        return false;
    }
}