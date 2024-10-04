using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures;

namespace Game.Combat.Spells;

public class InvisibleSpell : Spell<InvisibleSpell>
{
    public InvisibleSpell(uint duration)
    {
        Duration = duration;
    }

    public InvisibleSpell(uint duration, EffectT effect)
    {
        Duration = duration;
        Effect = effect;
    }

    public override string Name => "Invisible";
    public override EffectT Effect { get; } = EffectT.GlitterBlue;
    public override uint Duration { get; } = 10000;
    public override ushort Mana => 60;
    public override ConditionType ConditionType => ConditionType.Invisible;

    public override bool OnCast(ICombatActor actor, string words, out InvalidOperation error)
    {
        error = InvalidOperation.None;

        actor.TurnInvisible();
        return true;
    }

    public override void OnEnd(ICombatActor actor)
    {
        actor.TurnVisible();
        base.OnEnd(actor);
    }
}