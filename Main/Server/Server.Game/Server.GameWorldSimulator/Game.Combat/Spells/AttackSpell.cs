﻿using Game.Combat.Attacks;
using Server.Entities.Common;
using Server.Entities.Common.Combat.Structs;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Item;

namespace Game.Combat.Spells;

public abstract class AttackSpell : Spell<AttackSpell>
{
    public override EffectT Effect => EffectT.GlitterBlue;
    public override uint Duration => 0;
    public abstract DamageType DamageType { get; }
    public override ConditionType ConditionType => ConditionType.None;
    public abstract CombatAttack CombatAttack { get; }
    public virtual byte Range => 0;
    public virtual bool NeedsTarget => false;
    public virtual EffectT DamageEffect { get; }
    public abstract MinMax CalculateDamage(ICombatActor actor);

    public override bool OnCast(ICombatActor actor, string words, out InvalidOperation error)
    {
        error = InvalidOperation.None;

        var target = actor.CurrentTarget as ICombatActor;

        if (actor.Tile.ProtectionZone)
        {
            error = InvalidOperation.NotPermittedInProtectionZone;
            return false;
        }

        if (NeedsTarget && target is null)
        {
            error = InvalidOperation.NotPossible;
            return false;
        }

        var damage = CalculateDamage(actor);

        var result = actor.Attack(target, CombatAttack, new CombatAttackValue
        {
            Range = Range,
            DamageType = DamageType,
            DamageEffect = DamageEffect,
            MaxDamage = (ushort)damage.Max,
            MinDamage = (ushort)damage.Min
        });

        error = result.Error;
        return result.Succeeded;
    }
}