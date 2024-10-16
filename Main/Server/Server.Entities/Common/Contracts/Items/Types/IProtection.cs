using Server.Entities.Common.Combat.Structs;

namespace Server.Entities.Common.Contracts.Items.Types;

public interface IProtection
{
    bool Protect(ref CombatDamage damage);
}