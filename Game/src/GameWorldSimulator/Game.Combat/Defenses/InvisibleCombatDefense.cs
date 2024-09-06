using Game.Combat.Spells;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Spells;
using Game.Common.Creatures;

namespace Game.Combat.Defenses;

public class InvisibleCombatDefense : BaseCombatDefense
{
    public InvisibleCombatDefense(uint duration, EffectT effect)
    {
        Spell = new InvisibleSpell(duration, effect);
    }

    public ISpell Spell { get; }

    public override void Defend(ICombatActor actor)
    {
        Spell?.Invoke(actor, null, out var error);
    }
}