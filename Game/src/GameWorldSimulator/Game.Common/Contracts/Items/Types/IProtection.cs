using Game.Common.Combat.Structs;

namespace Game.Common.Contracts.Items.Types;

public interface IProtection
{
    bool Protect(ref CombatDamage damage);
}