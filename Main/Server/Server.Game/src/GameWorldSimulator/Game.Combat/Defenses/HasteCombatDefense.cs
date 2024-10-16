using Game.Combat.Spells;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Spells;
using Server.Entities.Common.Creatures;

namespace Game.Combat.Defenses;

public class HasteCombatDefense : BaseCombatDefense
{
    public HasteCombatDefense(uint duration, ushort speedBoost, EffectT effect)
    {
        Spell = new HasteSpell(duration, speedBoost, effect);
    }

    public ISpell Spell { get; }

    public override void Defend(ICombatActor actor)
    {
        Spell?.Invoke(actor, null, out var error);
    }
}