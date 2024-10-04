using Server.Entities.Common.Combat.Structs;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Item;

namespace Game.Combat.Attacks;

public class DrainCombatAttack : DistanceAreaCombatAttack
{
    public DrainCombatAttack(byte range, byte radius, ShootType shootType) : base(range, radius, shootType)
    {
    }

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

            enemy.ReceiveAttack(actor, damage);
            return true;
        }

        return false;
    }
}