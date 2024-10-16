using System.Linq;
using Server.Entities.Common.Combat.Structs;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Effects.Magical;

namespace Game.Combat.Attacks;

public class ExplosionCombatAttack : CombatAttack
{
    public ExplosionCombatAttack(byte radius)
    {
        Radius = radius;
    }

    public byte Radius { get; set; }

    public override bool TryAttack(ICombatActor actor, ICombatActor enemy, CombatAttackValue option,
        out CombatAttackResult combatResult)
    {
        combatResult = new CombatAttackResult
        {
            EffectT = option.DamageEffect
        };

        if (CalculateAttack(actor, enemy, option, out var damage))
        {
            combatResult.DamageType = option.DamageType;
            var location = enemy?.Location ?? actor.Location;

            var area = ExplosionEffect.Create(location, Radius).ToArray();

            combatResult.SetArea(area);
            actor.PropagateAttack(combatResult.Area, damage);
            return true;
        }

        return false;
    }
}