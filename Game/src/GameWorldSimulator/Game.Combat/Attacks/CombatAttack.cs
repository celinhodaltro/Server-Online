using Game.Common.Combat.Structs;
using Game.Common.Contracts.Combat.Attacks;
using Game.Common.Contracts.Creatures;
using Game.Common.Helpers;

namespace Game.Combat.Attacks;

public abstract class CombatAttack : ICombatAttack
{
    public virtual bool TryAttack(ICombatActor actor, ICombatActor enemy, CombatAttackValue option,
        out CombatAttackResult combatResult)
    {
        combatResult = new CombatAttackResult();
        return false;
    }

    public static bool CalculateAttack(ICombatActor actor, ICombatActor enemy, CombatAttackValue option,
        out CombatDamage damage)
    {
        damage = new CombatDamage();

        var damageValue = (ushort)GameRandom.Random.NextInRange(option.MinDamage, option.MaxDamage);
        damage = new CombatDamage(damageValue, option.DamageType);

        return true;
    }
}