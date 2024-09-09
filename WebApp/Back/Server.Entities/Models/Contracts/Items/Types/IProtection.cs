using Server.Entities.Models.Combat.Structs;

namespace Server.Entities.Models.Contracts.Items.Types;

public interface IProtection
{
    bool Protect(ref CombatDamage damage);
}