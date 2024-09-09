using Server.Entities.Models.Creatures.Structs;
using Server.Entities.Models.Combat.Structs;
using Server.Entities.Models.Item;

namespace Server.Entities.Models.Contracts.Combat.Attacks;

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