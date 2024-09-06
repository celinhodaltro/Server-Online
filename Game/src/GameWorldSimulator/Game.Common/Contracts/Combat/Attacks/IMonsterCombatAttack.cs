using Game.Common.Combat.Structs;
using Game.Common.Creatures.Structs;
using Game.Common.Item;

namespace Game.Common.Contracts.Combat.Attacks;

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