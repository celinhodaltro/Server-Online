using Server.Entities.Common.Combat.Structs;
using Server.Entities.Common.Creatures.Structs;
using Server.Entities.Common.Item;

namespace Server.Entities.Common.Contracts.Combat.Attacks;

public interface IMonsterCombatAttack
{
    byte Chance { get; set; }
    ICombatAttack CombatAttack { get; set; }
    DamageType DamageType { get; set; }
    int Interval { set; }
    byte Target { get; set; }

    CooldownTime Cooldown { get; }

    CombatAttackValue Translate();
}