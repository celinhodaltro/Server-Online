using Server.Entities.Common.Combat.Structs;
using Server.Entities.Common.Contracts.Creatures;

namespace Server.Entities.Common.Contracts.Combat.Attacks;

public interface ICombatAttack
{
    bool TryAttack(ICombatActor actor, ICombatActor enemy, CombatAttackValue option,
        out CombatAttackResult combatResult);
}