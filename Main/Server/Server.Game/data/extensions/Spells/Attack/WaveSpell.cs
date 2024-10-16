using Game.Combat.Attacks;
using Game.Combat.Spells;
using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Contracts;
using Server.Helpers;

namespace Extensions.Spells.Attack;

public abstract class WaveSpell : AttackSpell
{
    private AreaCombatAttack _areaCombatAttack;
    protected abstract string AreaName { get; }

    public override CombatAttack CombatAttack => _areaCombatAttack;

    public override bool OnCast(ICombatActor actor, string words, out InvalidOperation error)
    {
        var effectStore = IoC.GetInstance<IAreaEffectStore>();
        var area = effectStore.Get(AreaName, actor.Direction);

        _areaCombatAttack = new AreaCombatAttack(area);

        return base.OnCast(actor, words, out error);
    }
}