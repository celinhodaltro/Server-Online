using System;
using Server.Entities.Models.Contracts.Items.Types;
using Server.Entities.Models.Item;

namespace Server.Entities.Models.Contracts.Items.Types.Body;

public interface IAmmoEquipment : ICumulative, IBodyEquipmentEquipment
{
    byte Attack { get; }
    byte ExtraHitChance { get; }
    AmmoType AmmoType { get; }
    ShootType ShootType { get; }
    bool HasElementalDamage { get; }
    Tuple<DamageType, byte> ElementalDamage { get; }

    void Throw();
}