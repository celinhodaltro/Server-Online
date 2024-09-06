using Game.Combat.Attacks;
using Game.Combat.Spells;
using Game.Common;
using Game.Common.Contracts.Creatures;
using Game.Common.Creatures;
using Game.Common.Item;

namespace Extensions.Spells.Attack.Knight;

public class WhirlwindThrow : AttackSpell
{
    private CombatAttack _distanceAttack;
    public override DamageType DamageType => DamageType.MagicalPhysical;
    public override CombatAttack CombatAttack => _distanceAttack;
    public override byte Range => 5;
    public override bool NeedsTarget => true;

    public override MinMax CalculateDamage(ICombatActor actor)
    {
        return new MinMax(5, 100);
    }

    public override bool OnCast(ICombatActor actor, string words, out InvalidOperation error)
    {
        error = InvalidOperation.NotPossible;
        if (actor is not IPlayer player) return false;

        var shootType = player.SkillInUse switch
        {
            SkillType.Axe => ShootType.WhirlwindAxe,
            SkillType.Club => ShootType.WhirlwindClub,
            SkillType.Sword => ShootType.WhirlwindSword,
            _ => ShootType.None
        };

        if (shootType is ShootType.None) return false;

        _distanceAttack = new DistanceCombatAttack(Range, shootType);

        return base.OnCast(actor, words, out error);
    }
}