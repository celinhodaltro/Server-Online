using Game.Combat.Attacks;
using Game.Combat.Spells;
using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures;
using Server.Entities.Common.Item;

namespace Extensions.Spells.Attack;

public class Berserk : AttackSpell
{
    public override CombatAttack CombatAttack => new ExplosionCombatAttack(3);
    public override uint Duration => default;
    public override DamageType DamageType => DamageType.MagicalPhysical;
    public override ConditionType ConditionType => ConditionType.None;

    public override MinMax CalculateDamage(ICombatActor actor)
    {
        return new MinMax(5, 100);
    }
}