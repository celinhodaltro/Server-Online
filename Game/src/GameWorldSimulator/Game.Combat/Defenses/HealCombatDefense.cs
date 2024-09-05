using Game.Combat.Spells;
using Game.Common;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Spells;
using Game.Common.Creatures;

namespace Game.Combat.Defenses;

public class HealCombatDefense : BaseCombatDefense
{
    public HealCombatDefense(int min, int max, EffectT effect) //todo: remove dataManager from here
    {
        Spell = new HealSpell(new MinMax(min, max), effect);
    }

    public ISpell Spell { get; }

    public override void Defend(ICombatActor actor)
    {
        Spell?.Invoke(actor, null, out var error);
    }
}