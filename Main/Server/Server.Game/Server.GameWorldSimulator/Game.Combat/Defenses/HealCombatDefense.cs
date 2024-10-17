using Game.Combat.Spells;
using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts.Spells;
using Server.Entities.Common.Creatures;

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