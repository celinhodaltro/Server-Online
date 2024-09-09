using Server.Entities.Models.Combat.Structs;
using Server.Entities.Models.Contracts.Creatures;

namespace Server.Entities.Models.Contracts.Combat.Attacks;

public interface ICombatAttack
{
    bool TryAttack(ICombatActor actor, ICombatActor enemy, CombatAttackValue option,
        out CombatAttackResult combatResult);
}