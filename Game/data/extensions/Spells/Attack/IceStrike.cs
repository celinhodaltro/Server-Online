using Game.Combat.Attacks;
using Game.Combat.Spells;
using Game.Common;
using Game.Common.Contracts.Creatures;
using Game.Common.Item;

namespace Extensions.Spells.Attack;

public class IceStrike : AttackSpell
{
    public override DamageType DamageType => DamageType.Ice;
    public override CombatAttack CombatAttack => new DistanceCombatAttack(Range, ShootType.Ice);
    public override byte Range => 5;

    public override MinMax CalculateDamage(ICombatActor actor)
    {
        return new MinMax(5, 100);
    }
}