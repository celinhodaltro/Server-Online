using Server.Entities.Models.Creatures;
using Server.Entities.Models.Contracts.Items;
using Server.Entities.Models.Item;

namespace Server.Entities.Models.Contracts.Items.Types.Usable;

public interface IUsableOn : IItem
{
    public EffectT Effect => Metadata.Attributes.GetEffect();

    public int CooldownTime => Metadata.Attributes.HasAttribute(ItemAttribute.CooldownTime)
        ? Metadata.Attributes.GetAttribute<int>(ItemAttribute.CooldownTime)
        : 1000;
}