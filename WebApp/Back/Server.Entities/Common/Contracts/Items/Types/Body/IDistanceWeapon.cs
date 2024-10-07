using Server.Entities.Common.Item;

namespace Server.Entities.Common.Contracts.Items.Types.Body;

public interface IDistanceWeapon : IWeapon
{
    byte ExtraAttack => Metadata.Attributes.GetAttribute<byte>(ItemAttribute.Attack);
    sbyte ExtraHitChance => Metadata.Attributes.GetAttribute<sbyte>(ItemAttribute.HitChance);
    byte Range => Metadata.Attributes.GetAttribute<byte>(ItemAttribute.Range);
}