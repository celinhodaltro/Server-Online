using Game.Combat.Spells;
using Game.Common.Contracts.Creatures;
using Game.Common.Contracts.Spells;
using Game.Common.Creatures;

namespace Game.Combat.Defenses;

public class IllusionCombatDefense : BaseCombatDefense
{
    public IllusionCombatDefense(uint duration, string monsterName, EffectT effect,
        IMonsterDataManager dataManager) //todo: remove dataManager from here
    {
        Spell = new IllusionSpell(duration, monsterName, dataManager, effect);
    }

    public ISpell Spell { get; }

    public override void Defend(ICombatActor actor)
    {
        Spell?.Invoke(actor, null, out var error);
    }
}