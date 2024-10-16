﻿using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures;

namespace Game.Combat.Spells;

public class HasteSpell : Spell<HasteSpell>
{
    public HasteSpell(uint duration, ushort speedBoost, EffectT effect)
    {
        Effect = effect;
        SpeedBoost = speedBoost;
        Duration = duration;
    }

    public HasteSpell()
    {
    }

    public override string Name => "Haste";
    public override EffectT Effect { get; } = EffectT.GlitterBlue;
    public override uint Duration { get; } = 10000;
    public virtual ushort SpeedBoost { get; } = 200;
    public override ushort Mana => 60;
    public override ConditionType ConditionType => ConditionType.Haste;

    public override bool OnCast(ICombatActor actor, string words, out InvalidOperation error)
    {
        error = InvalidOperation.None;

        actor.IncreaseSpeed(SpeedBoost);
        return true;
    }

    public override void OnEnd(ICombatActor actor)
    {
        actor.DecreaseSpeed(SpeedBoost);
        base.OnEnd(actor);
    }
}