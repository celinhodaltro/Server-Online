using System;
using Server.Entities.Common.Combat.Structs;
using Server.Entities.Common.Contracts.Creatures;
using Server.Entities.Common.Creatures.Players;
using Server.Entities.Common.Item;

namespace Server.Entities.Common.Contracts.Items.Types.Body;

public delegate bool AttackEnemy(ICombatActor actor, ICombatActor enemy, DamageType damageType, int minDamage,
    int maxDamage, out CombatDamage damage);

public interface IWeapon : IBodyEquipmentEquipment
{
    bool TwoHanded => Metadata.BodyPosition == Slot.TwoHanded;

    new Slot Slot => Slot.Left;
    public WeaponType Type => Metadata.WeaponType;

    bool Attack(ICombatActor actor, ICombatActor enemy, out CombatAttackResult combat);
}

public interface IWeaponItem : IWeapon
{
    ushort AttackPower { get; }
    byte Defense => Metadata.Attributes.GetAttribute<byte>(ItemAttribute.Defense);

    Tuple<DamageType, byte> ElementalDamage { get; }
}