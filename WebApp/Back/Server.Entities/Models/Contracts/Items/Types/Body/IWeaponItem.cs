using System;
using Game.Common.Creatures.Players;
using Server.Entities.Models.Combat.Structs;
using Server.Entities.Models.Contracts.Creatures;
using Server.Entities.Models.Item;

namespace Server.Entities.Models.Contracts.Items.Types.Body;

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