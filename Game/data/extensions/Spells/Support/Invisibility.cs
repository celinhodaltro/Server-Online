using Game.Combat.Spells;
using Game.Common;
using Game.Common.Contracts.Creatures;
using Game.Common.Creatures;

namespace Extensions.Spells.Support;

public class Invisibility : Spell<Invisibility>
{
    public override EffectT Effect => EffectT.GlitterBlue;
    public override uint Duration => 20000;
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