using Game.Common.Combat.Structs;
using Game.Common.Contracts.Creatures;

namespace Game.Common.Contracts.Combat.Attacks;

public interface ICombatAttack
{
    bool TryAttack(ICombatActor actor, ICombatActor enemy, CombatAttackValue option,
        out CombatAttackResult combatResult);
}