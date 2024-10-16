using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Helpers;

namespace Game.Combat.Spells;

public class HealSpell : Spell<HealSpell>
{
    public HealSpell(MinMax minMax, EffectT effect)
    {
        Effect = effect;
        Min = (ushort)minMax.Min;
        Max = (ushort)minMax.Max;
    }

    public override string Name => "Healing";
    public override EffectT Effect { get; } = EffectT.GlitterBlue;
    public override ushort Mana => 60;
    public override ConditionType ConditionType => ConditionType.None;
    public virtual ushort Min { get; }
    public virtual ushort Max { get; }
    public override uint Duration => 0;

    public override bool OnCast(ICombatActor actor, string words, out InvalidOperation error)
    {
        error = InvalidOperation.None;

        var hpToIncrease = GameRandom.Random.NextInRange(Min, Max);
        actor.Heal((ushort)hpToIncrease, actor);
        return true;
    }

    public override void OnEnd(ICombatActor actor)
    {
        base.OnEnd(actor);
    }
}