using System.Linq;
using Game.Common.Combat.Structs;
using Game.Common.Contracts.Creatures;
using Game.Common.Effects.Magical;
using Game.Common.Item;

namespace Game.Combat.Attacks;

public class DistanceAreaCombatAttack : DistanceCombatAttack
{
    public DistanceAreaCombatAttack(byte range, byte radius, ShootType shootType) : base(range, shootType)
    {
        Radius = radius;
    }

    public byte Radius { get; set; }

    public override bool TryAttack(ICombatActor actor, ICombatActor enemy, CombatAttackValue option,
        out CombatAttackResult combatResult)
    {
        combatResult = new CombatAttackResult(ShootType)
        {
            EffectT = option.DamageEffect
        };

        if (CalculateAttack(actor, enemy, option, out var damage))
        {
            combatResult.DamageType = option.DamageType;
            combatResult.SetArea(ExplosionEffect.Create(enemy.Location, Radius).ToArray());
            actor.PropagateAttack(combatResult.Area, damage);
            return true;
        }

        return false;
    }
}