using Game.Common.Creatures;
using Game.Common.Item;

namespace Game.Common.Contracts.Items.Types.Usable;

public interface IUsableOn : IItem
{
    public EffectT Effect => Metadata.Attributes.GetEffect();

    public int CooldownTime => Metadata.Attributes.HasAttribute(ItemAttribute.CooldownTime)
        ? Metadata.Attributes.GetAttribute<int>(ItemAttribute.CooldownTime)
        : 1000;
}