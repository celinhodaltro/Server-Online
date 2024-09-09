using Server.Entities.Models.Contracts.Items;
using Server.Entities.Models.Item;

namespace Server.Entities.Models.Contracts.Items.Types.Body;

public interface IDefenseEquipment : IBodyEquipmentEquipment, IEquipment
{
    ushort DefenseValue => Metadata.Attributes.HasAttribute(ItemAttribute.Defense)
        ? Metadata.Attributes.GetAttribute<byte>(ItemAttribute.Defense)
        : Metadata.Attributes.GetAttribute<byte>(ItemAttribute.Armor);

    ushort ArmorValue => Metadata.Attributes.GetAttribute<byte>(ItemAttribute.Armor);
}