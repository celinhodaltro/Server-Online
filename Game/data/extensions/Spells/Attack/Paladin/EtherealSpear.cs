using Game.Combat.Attacks;
using Game.Combat.Spells;
using Server.Entities.Common;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Item;

namespace Extensions.Spells.Attack.Paladin;

public class EtherealSpear : AttackSpell
{
    public override DamageType DamageType => DamageType.MagicalPhysical;
    public override CombatAttack CombatAttack => new DistanceCombatAttack(Range, ShootType.EtherealSpear);
    public override byte Range => 5;
    public override bool NeedsTarget => true;

    public override MinMax CalculateDamage(ICombatActor actor)
    {
        return new MinMax(5, 100);
    }
}